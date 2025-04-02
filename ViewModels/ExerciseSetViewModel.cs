using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using D424.Classes;
using D424.Helpers;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;

namespace D424.ViewModels;

public partial class ExerciseSetViewModel : ObservableObject
{
    private readonly DatabaseHelper dbHelper;
    private readonly RoutineViewModel routineVm;

    public ExerciseSetViewModel(DatabaseHelper dbHelper, RoutineViewModel routineVm)
    {
        this.dbHelper = dbHelper;
        this.routineVm = routineVm;
        LoadSets();
    }
    
    [ObservableProperty] private ObservableCollection<ExerciseSets> exerciseSets = [];

    [ObservableProperty] private ExerciseSets selectedSet;

    private int ExerciseId { get; set; }

    private async void LoadSets()
    {
        var exerciseSetsList = await dbHelper.GetSetsByExerciseIdAsync(ExerciseId);

        MainThread.BeginInvokeOnMainThread(() =>
        {
            ExerciseSets.Clear();
            foreach (var set in exerciseSetsList)
            {
                ExerciseSets.Add(set);
            }
        });
    }

    [RelayCommand]
    public async Task AddSet(Exercises exercise)
    {
        if (exercise == null)
        {
            return;
        }
        
        var lastSet = exercise.Id > 0 
            ? (await dbHelper.GetSetsByExerciseIdAsync(exercise.Id)).LastOrDefault()
            : null;
    
        var newSet = new ExerciseSets
        {
            ExerciseId = exercise.Id,
            SetNumber = exercise.ExerciseSets.Any()
                ? exercise.ExerciseSets.Max(s => s.SetNumber) + 1
                : 1,
            Previous = (lastSet != null && lastSet.Reps > 0) 
                ? $"{lastSet.Weight} lbs x {lastSet.Reps}" 
                : "-",
            Weight = 0,
            Reps = 0
        };
        
        MainThread.BeginInvokeOnMainThread(() =>
        {
            exercise.ExerciseSets.Add(newSet);
        });
    }
    
    [RelayCommand]
    public async Task DeleteSet(ExerciseSets setToDelete)
    {
        if (setToDelete == null)
        {
            return;
        }
        
        var parentExercise = routineVm.NewRoutine.Exercises.FirstOrDefault(e => e.ExerciseSets.Contains(setToDelete));
        if (parentExercise == null)
        {
            return;
        }
        
        if (parentExercise.ExerciseSets.Count == 1)
        {
            await Shell.Current.DisplayAlert("Warning", "Each exercise must have at least one set.", "OK");
            return;
        }
        
        MainThread.BeginInvokeOnMainThread(() =>
        {
            parentExercise.ExerciseSets.Remove(setToDelete);
        
            for (int i = 0; i < parentExercise.ExerciseSets.Count; i++)
            {
                parentExercise.ExerciseSets[i].SetNumber = i + 1;
            }
        });
    }
    
    [RelayCommand]
    public async Task AddUpdatedSet(Exercises exercise)
    {
        if (exercise == null)
        {
            return;
        }

        var lastSet = exercise.Id > 0 
            ? (await dbHelper.GetSetsByExerciseIdAsync(exercise.Id)).LastOrDefault()
            : null;
    
        var newSet = new ExerciseSets
        {
            ExerciseId = exercise.Id,
            SetNumber = exercise.ExerciseSets.Any()
                ? exercise.ExerciseSets.Max(s => s.SetNumber) + 1
                : 1,
            Previous = (lastSet != null && lastSet.Reps > 0) 
                ? $"{lastSet.Weight} lbs x {lastSet.Reps}" 
                : "-",
            Weight = 0,
            Reps = 0
        };

        MainThread.BeginInvokeOnMainThread(() =>
        {
            exercise.ExerciseSets.Add(newSet);
        });
    }

    [RelayCommand]
    public async Task DeleteUpdatedSet(ExerciseSets setToDelete)
    {
        if (setToDelete == null)
        {
            return;
        }

        var parentExercise = routineVm.EditedRoutine.Exercises.FirstOrDefault(e => e.ExerciseSets.Contains(setToDelete));
        if (parentExercise == null)
        {
            return;
        }

        if (parentExercise.ExerciseSets.Count == 1)
        {
            await Shell.Current.DisplayAlert("Warning", "Each exercise must have at least one set.", "OK");
            return;
        }
        
        MainThread.BeginInvokeOnMainThread(() =>
        {
            parentExercise.ExerciseSets.Remove(setToDelete);
            
            for (int i = 0; i < parentExercise.ExerciseSets.Count; i++)
            {
                parentExercise.ExerciseSets[i].SetNumber = i + 1;
            }
        });
    }
}
