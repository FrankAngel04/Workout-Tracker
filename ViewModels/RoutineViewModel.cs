using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using D424.Classes;
using D424.Helpers;
using D424.Pages;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Timer = System.Timers.Timer;

namespace D424.ViewModels;

public partial class RoutineViewModel : ObservableObject
{ 
    private readonly DatabaseHelper _dbHelper;
    private readonly ReportViewModel _reportVm;

    public RoutineViewModel(DatabaseHelper dbHelper, ReportViewModel reportVm)
    {
        _dbHelper = dbHelper;
        _reportVm = reportVm;
        ExerciseList = new ObservableCollection<Exercises>();
        
        MessagingCenter.Subscribe<LoginPage>(this, "RoutinesUpdated", async (sender) =>
        {
            Debug.WriteLine("🔄 Reloading routines after login...");
            await LoadRoutines();
        });

        _ = LoadRoutines();
    }
    
    [ObservableProperty]
    private ObservableCollection<Exercises> _exerciseList = [];
    
    [ObservableProperty]
    private ObservableCollection<Routines> _routines = [];
    
    [ObservableProperty]
    private Routines _newRoutine = null!;
    
    [ObservableProperty]
    private List<Exercises> _deletedExercises = [];
    
    private Timer? _workoutTimer;
    private DateTime _startTime;
    private int _routinesLoaded = 0;
    private bool isRefreshing = false;
    
    [ObservableProperty] 
    private Routines _originalRoutine = null!;
    
    [ObservableProperty]
    private Routines _editedRoutine = null!;

    [ObservableProperty]
    private string _workoutDuration = "00:00:00";

    public void InitializeNewRoutine()
    {
        NewRoutine = new Routines
        {
            Id = 0,
            Name = "",
            Exercises = new ObservableCollection<Exercises>()
        };
    }
    
    public async Task LoadRoutineForEditing(Routines routine)
    {
        OriginalRoutine = routine; 
        EditedRoutine = DeepCopyRoutine(routine); 
        NewRoutine = EditedRoutine;  
        
        EditedRoutine.Exercises = new ObservableCollection<Exercises>(
            await _dbHelper.GetExercisesByRoutineIdAsync(routine.Id)
        );
        
        foreach (var exercise in EditedRoutine.Exercises)
        {
            if (exercise.Id <= 0)
            {
                continue;
            }

            var sets = await _dbHelper.GetSetsByExerciseIdAsync(exercise.Id);
            exercise.ExerciseSets = new ObservableCollection<ExerciseSets>(sets);
        }
    }
    
    internal Routines DeepCopyRoutine(Routines routine)
    {
        var newRoutine = new Routines
        {
            Id = routine.Id,
            Name = routine.Name,
            Exercises = new ObservableCollection<Exercises>(
                routine.Exercises.Select(e => new Exercises
                {
                    Id = e.Id,
                    RoutineId = e.RoutineId,
                    name = e.name,
                    force = e.force,
                    level = e.level,
                    mechanic = e.mechanic,
                    equipment = e.equipment,
                    category = e.category,
                    ExerciseSets = new ObservableCollection<ExerciseSets>(
                        e.ExerciseSets.Select(set => new ExerciseSets
                        {
                            Id = set.Id,
                            ExerciseId = set.ExerciseId,
                            SetNumber = set.SetNumber,
                            Previous = set.Previous,
                            Weight = set.Weight,
                            Reps = set.Reps,
                            IsCompleted = set.IsCompleted
                        })
                    )
                })
            )
        };
        return newRoutine;
    }

    private async Task LoadRoutines()
    {
        int userId = Preferences.Get("UserId", 0);
        var routinesFromDb = await _dbHelper.GetRoutinesAsync();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            Routines.Clear();
            foreach (var routine in routinesFromDb.Where(r => r.UserId == userId))
            {
                Routines.Add(routine);
            }
        });
    }
    
    public async Task RefreshExerciseList()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            ExerciseList.Clear();
        });
    
        var exercises = await _dbHelper.GetExercisesByRoutineIdAsync(NewRoutine.Id);
    
        if (exercises.Count == 0)
        {
            return;
        }
    
        foreach (var exercise in exercises)
        {
            if (exercise.Id == 0)
            {
                continue;
            }
    
            var sets = await _dbHelper.GetSetsByExerciseIdAsync(exercise.Id);
    
            foreach (var set in sets)
            {
                if (set is { Weight: > 0, Reps: > 0 })
                {
                    set.Previous = $"{set.Weight} lbs x {set.Reps}";
                }
            }
    
            exercise.ExerciseSets = new ObservableCollection<ExerciseSets>(sets);
    
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (ExerciseList.All(e => e.Id != exercise.Id))
                {
                    ExerciseList.Add(exercise);
                }
            });
        }
    }
    
    private async Task RefreshRoutinesList(bool isNewRoutine)
    {
        var routinesFromDb = await _dbHelper.GetRoutinesAsync();
    
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Routines.Clear();
            foreach (var routine in routinesFromDb)
            {
                Routines.Add(routine);
            }
    
            if (isNewRoutine && routinesFromDb.Count > 0)
            {
                NewRoutine = routinesFromDb.Last();
            }
        });
    }
    
    [RelayCommand]
    private async Task SaveRoutine()
    {
        if (string.IsNullOrWhiteSpace(NewRoutine.Name))
        {
            await Shell.Current.DisplayAlert("Error", "Routine name is required!", "OK");
            return;
        }

        bool isNewRoutine = NewRoutine.Id == 0;

        int savedRoutineId = await _dbHelper.SaveRoutineAsync(NewRoutine);
        if (savedRoutineId <= 0)
        {
            return;
        }

        NewRoutine.Id = savedRoutineId;

        foreach (var exercise in NewRoutine.Exercises)
        {
            exercise.RoutineId = savedRoutineId;
            exercise.Id = await _dbHelper.SaveExerciseAsync(exercise);

            foreach (var set in exercise.ExerciseSets)
            {
                set.ExerciseId = exercise.Id;
                set.Id = await _dbHelper.SaveSetAsync(set);
            }
        }

        await RefreshRoutinesList(isNewRoutine);
        await RefreshExerciseList();

        await Shell.Current.GoToAsync("..");
    }
    
    [RelayCommand]
    private async Task SaveUpdateRoutine()
    {
        if (string.IsNullOrWhiteSpace(EditedRoutine.Name))
        {
            await Shell.Current.DisplayAlert("Error", "Routine name is required!", "OK");
            return;
        }

        int originalRoutineId = EditedRoutine.Id;
        if (originalRoutineId == 0)
        {
            return;
        }

        int savedRoutineId = await _dbHelper.SaveRoutineAsync(EditedRoutine);

        if (savedRoutineId == 0 || savedRoutineId != originalRoutineId)
        {
            EditedRoutine.Id = originalRoutineId; 
            return;
        }

        foreach (var deletedExercise in DeletedExercises)
        {
            await _dbHelper.DeleteExerciseAsync(deletedExercise);
        }
        DeletedExercises.Clear();

        List<int> savedExerciseIds = new();

        foreach (var exercise in EditedRoutine.Exercises)
        {
            exercise.RoutineId = originalRoutineId;

            if (exercise.Id == 0)
            {
                exercise.Id = await _dbHelper.SaveExerciseAsync(exercise);
            }
            else
            {
                await _dbHelper.SaveExerciseAsync(exercise);
            }

            savedExerciseIds.Add(exercise.Id);
        }

        await Task.Delay(300);

        foreach (var exercise in EditedRoutine.Exercises)
        {
            for (int i = 0; i < exercise.ExerciseSets.Count; i++)
            {
                var set = exercise.ExerciseSets[i];
                set.SetNumber = i + 1;
                set.ExerciseId = exercise.Id;

                if (set.Id == 0)
                {
                    set.Id = await _dbHelper.SaveSetAsync(set);
                }
                else
                {
                    await _dbHelper.SaveSetAsync(set);
                }
            }

            var existingSets = await _dbHelper.GetSetsByExerciseIdAsync(exercise.Id);
            var setsToDelete = existingSets.Where(s => exercise.ExerciseSets.All(ed => ed.Id != s.Id)).ToList();

            foreach (var set in setsToDelete)
            {
                await _dbHelper.DeleteSetAsync(set);
            }
        }

        await Task.Delay(500);
        var refreshedExercises = await _dbHelper.GetExercisesByRoutineIdAsync(originalRoutineId);

        MainThread.BeginInvokeOnMainThread(() =>
        {
            ExerciseList.Clear();
            foreach (var ex in refreshedExercises) ExerciseList.Add(ex);
        });
        
        await Shell.Current.GoToAsync("..");
    }
    
    [RelayCommand]
    private async Task EditRoutine(Routines routine)
    {
        if (routine == null)
        {
            return;
        }

        OriginalRoutine = DeepCopyRoutine(routine);

        EditedRoutine = DeepCopyRoutine(routine);

        EditedRoutine.Exercises = new ObservableCollection<Exercises>(
            await _dbHelper.GetExercisesByRoutineIdAsync(routine.Id)
        );

        foreach (var exercise in EditedRoutine.Exercises)
        {
            var sets = await _dbHelper.GetSetsByExerciseIdAsync(exercise.Id);
            exercise.ExerciseSets = new ObservableCollection<ExerciseSets>(sets);
        }

        await Shell.Current.Navigation.PushAsync(new EditRoutinePage(_dbHelper, this, 
            new ExerciseViewModel(_dbHelper, new ExerciseHelper(), this), 
            new ExerciseSetViewModel(_dbHelper, this)));
    }
    
    [RelayCommand]
    private async Task DeleteRoutine(Routines routine)
    {
        var result = await Shell.Current.DisplayAlert("Delete Routine", $"Are you sure you want to delete {routine.Name}?", "Yes", "No");
        if (!result) return;

        var exercises = await _dbHelper.GetExercisesByRoutineIdAsync(routine.Id);
        foreach (var exercise in exercises)
        {
            var sets = await _dbHelper.GetSetsByExerciseIdAsync(exercise.Id);
            foreach (var set in sets)
            {
                await _dbHelper.DeleteSetAsync(set);
            }
            await _dbHelper.DeleteExerciseAsync(exercise);
        }

        await _dbHelper.DeleteRoutineAsync(routine);
        MainThread.BeginInvokeOnMainThread(() => Routines.Remove(routine));
    }
    
    private void StartWorkoutTimer()
    {
        _startTime = DateTime.Now;

        _workoutTimer = new Timer(1000);
        _workoutTimer.Elapsed += (_, _) =>
        {
            var elapsed = DateTime.Now - _startTime;
            WorkoutDuration = elapsed.ToString(@"hh\:mm\:ss");
        };
        _workoutTimer.Start();
    }

    [RelayCommand]
    private async Task TapRoutine(Routines routine)
    {
        NewRoutine = routine;
    
        StartWorkoutTimer();

        await Application.Current!.MainPage!.Navigation.PushAsync(new StartRoutinePage(this));

        await RefreshExerciseList();
    }
    
    private void StopWorkoutTimer()
    {
        if (_workoutTimer != null)
        {
            _workoutTimer.Stop();
            _workoutTimer.Dispose();
            _workoutTimer = null;
        }
    }
    
    [RelayCommand]
    private async Task FinishAndSaveRoutine()
    {
        if (ExerciseList.Count == 0)
        {
            await Shell.Current.DisplayAlert("Error", "No routine data found!", "OK");
            return;
        }

        foreach (var exercise in ExerciseList)
        {
            foreach (var set in exercise.ExerciseSets)
            {
                set.Weight = set.Weight;
                set.Reps = set.Reps;

                await _dbHelper.SaveSetAsync(set);
            }
            await _dbHelper.SaveExerciseAsync(exercise);
        }
        
        StopWorkoutTimer();
        var finalWorkoutDuration = WorkoutDuration;

        var report = new Reports
        {
            RoutineId = NewRoutine.Id,
            RoutineName = NewRoutine.Name,
            WorkoutDuration = finalWorkoutDuration,
            CompletedOn = DateTime.Now
        };

        report.Id = await _dbHelper.SaveWorkoutReportAsync(report);

        foreach (var exercise in ExerciseList)
        {
            var completedExercise = new CompletedExercises()
            {
                ReportId = report.Id,
                name = exercise.name,
                force = exercise.force,
                level = exercise.level,
                mechanic = exercise.mechanic,
                equipment = exercise.equipment,
                category = exercise.category
            };

            completedExercise.Id = await _dbHelper.SaveExerciseAsync(completedExercise);

            foreach (var set in exercise.ExerciseSets)
            {
                var completedSet = new CompletedSets
                {
                    CompletedExerciseId = completedExercise.Id,
                    SetNumber = set.SetNumber,
                    Weight = set.Weight,
                    Reps = set.Reps
                };

                await _dbHelper.SaveSetAsync(completedSet);
            }
        }

        foreach (var exercise in ExerciseList)
        {
            foreach (var set in exercise.ExerciseSets)
            {
                set.IsCompleted = false;
                await _dbHelper.SaveSetAsync(set);
            }
        }

        _reportVm.RefreshReports();
        
        await Shell.Current.DisplayAlert("Workout Completed", "Workout saved successfully!", "OK");
        await Task.WhenAny(
            Shell.Current.Navigation.PopAsync(),
            Task.Delay(200)
        );
        await Shell.Current.GoToAsync("//ReportPage");
    }
}