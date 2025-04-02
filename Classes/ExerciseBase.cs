using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using SQLite;

namespace D424.Classes;

public class ExerciseBase
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoutineId { get; set; }
    public string name { get; set; }
    public string force { get; set; }
    public string level { get; set; }
    public string mechanic { get; set; }
    public string equipment { get; set; }
    public string category { get; set; }
    
    public virtual async Task<int> SaveToDatabase(SQLiteAsyncConnection database)
    {
        var existingExercise = await database.FindAsync<ExerciseBase>(Id);
    
        if (existingExercise == null) 
        {
            if (this is Exercises exercise) 
            {
                var existing = await database.Table<Exercises>()
                    .Where(e => e.name == name && e.RoutineId == exercise.RoutineId && e.UserId == exercise.UserId)
                    .FirstOrDefaultAsync();
        
                if (existing != null) 
                {
                    Debug.WriteLine($"Existing exercise found. Keeping current ID.");
                } 
                else 
                {
                    Id = 0;
                }
            }
        }

        if (Id == 0)
        {
            UserId = Preferences.Get("UserId", 0);
            Debug.WriteLine($"Inserting new exercise: {name} (User ID: {UserId})");
            await database.InsertAsync(this);
            Debug.WriteLine($"Insert successful. New exercise ID: {Id}, User ID: {UserId}");
        }
        else
        {
            Debug.WriteLine($"Updating existing exercise ID: {Id} (User ID: {UserId})");
            await database.UpdateAsync(this);
        }

        return Id;
    }


    
    [Ignore]
    public ObservableCollection<string> primaryMuscles { get; set; } = new();
    public string PrimaryMusclesDisplay => primaryMuscles != null ? string.Join(", ", primaryMuscles) : string.Empty;
    
    // Future use
    [Ignore] 
    private ObservableCollection<string> SecondaryMuscles { get; set; } = new();
    public string SecondaryMusclesDisplay => SecondaryMuscles != null ? string.Join(", ", SecondaryMuscles) : string.Empty;
    
    [Ignore]
    public ObservableCollection<string> _instructions { get; set; } = new();
    public ObservableCollection<string> Instructions
    {
        get => _instructions;
        private set => _instructions = value;
    }
    
    [Ignore]
    public ObservableCollection<string> Images { get; set; } = new();
}