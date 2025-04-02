using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using D424.Classes;
using D424.ViewModels;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Storage;
using SQLite;

namespace D424.Helpers;

public class DatabaseHelper
{
    private static readonly string DbPath = Path.Combine(FileSystem.AppDataDirectory, "WorkoutTrackerApp.db3");
    private readonly SQLiteAsyncConnection _database;
    
    public DatabaseHelper()
    {
        if (_database == null)
            _database = new SQLiteAsyncConnection(DbPath);
        
        _database.CreateTableAsync<Routines>().Wait();
        _database.CreateTableAsync<ExerciseBase>().Wait(); 
        _database.CreateTableAsync<Exercises>().Wait();
        _database.CreateTableAsync<ExerciseSetBase>().Wait();
        _database.CreateTableAsync<ExerciseSets>().Wait();
        _database.CreateTableAsync<Reports>().Wait();
        _database.CreateTableAsync<CompletedExercises>().Wait();
        _database.CreateTableAsync<CompletedSets>().Wait();
        
    }
    
    public async Task InitializeDatabaseAsync()
    {
        await _database.CreateTableAsync<User>();
    
        await _database.ExecuteAsync("CREATE INDEX IF NOT EXISTS idx_username ON User (Username)");
        await _database.ExecuteAsync("CREATE INDEX IF NOT EXISTS idx_routine_user ON Routines (UserId)");
        await _database.ExecuteAsync("CREATE INDEX IF NOT EXISTS idx_exercise_routine ON Exercises (RoutineId)");
        
        await AddUserIdColumnIfMissing("CompletedExercises");
        await AddUserIdColumnIfMissing("Exercises");
    }
    
    private async Task AddUserIdColumnIfMissing(string tableName)
    {
        var columnExists = await _database.ExecuteScalarAsync<int>(
            $"SELECT COUNT(*) FROM pragma_table_info('{tableName}') WHERE name='UserId'"
        );

        if (columnExists == 0)
        {
            await _database.ExecuteAsync($"ALTER TABLE {tableName} ADD COLUMN UserId INTEGER DEFAULT 0");
        }
    }
    
    public Task<List<Routines>> GetRoutinesAsync()
    {
        int userId = Preferences.Get("UserId", 0);

        return _database.Table<Routines>()
            .Where(r => r.UserId == userId)
            .ToListAsync();
    }
    
    public async Task<List<Exercises>> GetExercisesByRoutineIdAsync(int routineId, int limit = -1)
    {
        int userId = Preferences.Get("UserId", 0);

        var query = _database.Table<Exercises>()
            .Where(e => e.RoutineId == routineId && e.UserId == userId);

        var result = limit > 0 ? await query.Take(limit).ToListAsync() : await query.ToListAsync();

        return result;
    }
    
    public async Task<List<ExerciseSets>> GetSetsByExerciseIdAsync(int exerciseId)
    {
        var sets = await _database.Table<ExerciseSets>().Where(s => s.ExerciseId == exerciseId).ToListAsync();

        return sets;
    }
    
    public Task<List<CompletedExercises>> GetCompletedExercisesAsync(int reportId)
    {
        int userId = Preferences.Get("UserId", 0);
        return _database.Table<CompletedExercises>()
            .Where(e => e.ReportId == reportId && e.UserId == userId)
            .ToListAsync();
    }
    
    public Task<List<CompletedSets>> GetCompletedSetsAsync(int completedExerciseId) =>
        _database.Table<CompletedSets>().Where(s => s.CompletedExerciseId == completedExerciseId).ToListAsync();
    
    public Task<List<Reports>> GetWorkoutReportsAsync()
    {
        int userId = Preferences.Get("UserId", 0);
        return _database.Table<Reports>()
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.CompletedOn)
            .ToListAsync();
    }
    
    public async Task<int> SaveRoutineAsync(Routines routine)
    {
        routine.UserId = Preferences.Get("UserId", 0);

        if (routine.Id != 0)
        {
            await _database.UpdateAsync(routine);
            return routine.Id;
        }
        
        await _database.InsertAsync(routine);

        var newId = await _database.ExecuteScalarAsync<int>("SELECT last_insert_rowid()");
        routine.Id = newId;

        return newId;
    }
    
    public async Task<int> SaveExerciseAsync(ExerciseBase exercise)
    {
        exercise.UserId = Preferences.Get("UserId", 0);

        int result = await exercise.SaveToDatabase(_database);

        return result;
    }
    
    public async Task<int> SaveSetAsync(ExerciseSetBase set)
    {
        int result = await set.SaveToDatabase(_database);

        return result;
    }
    
    public async Task<int> SaveWorkoutReportAsync(Reports report)
    {
        report.UserId = Preferences.Get("UserId", 0);
    
        if (report.Id != 0)
        {
            await _database.UpdateAsync(report);
            return report.Id;
        }

        await _database.InsertAsync(report);

        report.Id = await _database.ExecuteScalarAsync<int>("SELECT last_insert_rowid()");
    
        return report.Id;
    }
    
    public Task<int> DeleteRoutineAsync(Routines routine) => _database.DeleteAsync(routine);

    public async Task<int> DeleteExerciseAsync(Exercises exercise)
    {
        if (exercise == null || exercise.Id <= 0)
        {
            return 0;
        }

        var sets = await GetSetsByExerciseIdAsync(exercise.Id);
        foreach (var set in sets)
        {
            await DeleteSetAsync(set);
        }

        await _database.ExecuteAsync("DELETE FROM ExerciseSets WHERE ExerciseId = ?", exercise.Id);

        int rowsDeleted = await _database.DeleteAsync(exercise);

        return rowsDeleted;
    }
    
    public async Task<int> DeleteSetAsync(ExerciseSets set)
    {
        if (set == null || set.Id <= 0)
        {
            return 0;
        }
        
        int rowsAffected = await _database.DeleteAsync(set);
    
        return rowsAffected;
    }
    
    public async Task<int> DeleteWorkoutReportAsync(Reports workout)
    {
        if (workout == null || workout.Id <= 0)
        {
            return 0;
        }

        var completedExercises = await GetCompletedExercisesAsync(workout.Id);

        foreach (var exercise in completedExercises)
        {
            var completedSets = await GetCompletedSetsAsync(exercise.Id);
            
            foreach (var set in completedSets)
            {
                await _database.DeleteAsync(set);
            }
            
            await _database.DeleteAsync(exercise);
        }
        
        int rowsAffected = await _database.DeleteAsync(workout);

        return rowsAffected;
    }
    
    public async Task<bool> RegisterUserAsync(string username, string password)
    {
        string normalizedUsername = username.Trim().ToLower();

        var existingUser = await _database.Table<User>()
            .Where(u => u.Username == normalizedUsername)
            .FirstOrDefaultAsync();

        if (existingUser != null)
        {
            return false;
        }

        var newUser = new User
        {
            Username = normalizedUsername,
            PasswordHash = User.HashPassword(password)
        };

        int result = await _database.InsertAsync(newUser);
        return result > 0;
    }
    
    private static readonly SemaphoreSlim _loginLock = new(1, 1);

    public async Task<(User user, int lockoutTime, int remainingAttempts)> LoginUserAsync(string username, string password)
    {
        
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return (null, 0, 0);
        }

        string normalizedUsername = username.Trim().ToLower();

        await _loginLock.WaitAsync();
        try
        {
            return await LoginUserInternalAsync(normalizedUsername, password);
        }
        finally
        {
            _loginLock.Release();
        }
    }

    private async Task<(User user, int lockoutTime, int remainingAttempts)> LoginUserInternalAsync(string normalizedUsername, string password)
    {
        var user = await GetUserByUsernameAsync(normalizedUsername);
        if (user == null)
        {
            return (null, 0, 5);
        }

        user = await GetUserByIdAsync(user.Id);

        int lockoutTime = GetRemainingLockoutTime(user);
        if (lockoutTime > 0)
        {
            return (null, lockoutTime, 0);
        }

        bool isPasswordCorrect = user.VerifyPassword(password);

        if (!isPasswordCorrect)
        {
            await IncrementFailedAttempts(user);

            user = await GetUserByUsernameAsync(normalizedUsername);
        
            int remainingAttempts = Math.Max(5 - user.FailedAttempts, 0);

            return (null, GetRemainingLockoutTime(user), remainingAttempts);
        }

        ResetFailedAttempts(user);
        await UpdateUserAsync(user);
        return (user, 0, 5);
    }
    
    private int GetRemainingLockoutTime(User user)
    {
        if (user.FailedAttempts < 5 || !user.LastFailedAttempt.HasValue)
            return 0;

        int elapsedSeconds = (int)DateTime.UtcNow.Subtract(user.LastFailedAttempt.Value).TotalSeconds;
        int lockoutTime = Math.Max(900 - elapsedSeconds, 0);
        return lockoutTime;
    }
    
    private void ResetFailedAttempts(User user)
    {
        user.FailedAttempts = 0;
        user.LastFailedAttempt = null;
    }
    
    private async Task<int> IncrementFailedAttempts(User user)
    {
        user.FailedAttempts++;
        user.LastFailedAttempt = DateTime.UtcNow;
        
        int rowsUpdated = await _database.ExecuteAsync(
            "UPDATE User SET FailedAttempts = ?, LastFailedAttempt = ? WHERE Id = ?",
            user.FailedAttempts, user.LastFailedAttempt, user.Id
        );

        return rowsUpdated;
    }

    
    public async Task<User> GetUserByIdAsync(int userId)
    {
        return await _database.Table<User>().Where(u => u.Id == userId).FirstOrDefaultAsync();
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _database.QueryAsync<User>
            ("SELECT * FROM User WHERE Username = ?", username)
            .ContinueWith(t => t.Result.FirstOrDefault());
    }

    internal async Task<int> UpdateUserAsync(User user)
    {
        Debug.WriteLine($"🔄 Updating User: {user.Username} (ID: {user.Id})");

        int rowsUpdated = await _database.ExecuteAsync(
            "UPDATE User SET Username = ?, PasswordHash = ?, FailedAttempts = ?, LastFailedAttempt = ? WHERE Id = ?",
            user.Username.ToLower(), user.PasswordHash, user.FailedAttempts, user.LastFailedAttempt, user.Id
        );

        return rowsUpdated;
    }





    internal async Task DeleteUserAccountAsync(int userId)
    {
        if (userId <= 0)
        {
            return;
        }
        
        await _database.ExecuteAsync("DELETE FROM Routines WHERE UserId = ?", userId);
        await _database.ExecuteAsync("DELETE FROM Reports WHERE UserId = ?", userId);
        await _database.ExecuteAsync("DELETE FROM Exercises WHERE RoutineId IN (SELECT Id FROM Routines WHERE UserId = ?)", userId);
        await _database.ExecuteAsync("DELETE FROM ExerciseSets WHERE ExerciseId IN (SELECT Id FROM Exercises WHERE RoutineId IN (SELECT Id FROM Routines WHERE UserId = ?))", userId);
        await _database.ExecuteAsync("DELETE FROM CompletedExercises WHERE ReportId IN (SELECT Id FROM Reports WHERE UserId = ?)", userId);
        await _database.ExecuteAsync("DELETE FROM CompletedSets WHERE CompletedExerciseId IN (SELECT Id FROM CompletedExercises WHERE ReportId IN (SELECT Id FROM Reports WHERE UserId = ?))", userId);
        
        await _database.ExecuteAsync("DELETE FROM User WHERE Id = ?", userId);
    }
}