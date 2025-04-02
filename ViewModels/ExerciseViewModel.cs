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
using BCrypt.Net;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;

namespace D424.ViewModels;

public partial class ExerciseViewModel : ObservableObject
{
    private readonly DatabaseHelper dbHelper;
    private readonly ExerciseHelper exerciseHelper;
    private readonly RoutineViewModel routineVm;

    public ExerciseViewModel(DatabaseHelper dbHelper, ExerciseHelper exerciseHelper, RoutineViewModel routineVm)
    {
        this.dbHelper = dbHelper;
        this.exerciseHelper = exerciseHelper;
        this.exerciseHelper = exerciseHelper;
        this.routineVm = routineVm;
        LoadExercises();
    }
    
    [ObservableProperty]
    private ObservableCollection<Exercises> availableExercises = [];

    [ObservableProperty]
    private ObservableCollection<Exercises> selectedExercises = [];
    
    private List<Exercises> _allExercises = new();

    private int RoutineId { get; set; }

    [ObservableProperty]
    private string searchText;

    [ObservableProperty]
    private bool isLoading;
    
    public void Initialize(int routineId)
    {
        RoutineId = routineId;
    }
    
    public async Task LoadExercises()
    {
        if (IsLoading) return;
        IsLoading = true;

        try
        {
            if (RoutineId > 0)
            {
                _allExercises = await dbHelper.GetExercisesByRoutineIdAsync(RoutineId);
            }
            else
            {
                _allExercises = await exerciseHelper.GetExercises();
            }

            MainThread.BeginInvokeOnMainThread(() =>
            {
                AvailableExercises.Clear();
                foreach (var exercise in _allExercises.Take(50))
                {
                    AvailableExercises.Add(exercise);
                }
            });

            int batchSize = 50;
            int count = AvailableExercises.Count;
            var remainingExercises = _allExercises.Skip(count).ToList();

            foreach (var batch in remainingExercises.Chunk(batchSize))
            {
                await Task.Delay(50);

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    foreach (var exercise in batch)
                    {
                        AvailableExercises.Add(exercise);
                    }
                });

                count += batch.Length;
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", "Failed to load exercises.", "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }
    
    partial void OnSearchTextChanged(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                AvailableExercises.Clear();
                foreach (var exercise in _allExercises)
                {
                    AvailableExercises.Add(exercise);
                }
            });
        }
        else
        {
            var filtered = _allExercises
                .Where(ex => ex.name.Contains(value, StringComparison.OrdinalIgnoreCase))
                .ToList();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                AvailableExercises.Clear();
                foreach (var exercise in filtered)
                {
                    AvailableExercises.Add(exercise);
                }
            });
        }
    }

    
    [RelayCommand]
    public async Task DeleteExercise(Exercises exercise)
    {
        if (routineVm.NewRoutine == null) return;

        bool confirm = await Shell.Current.DisplayAlert(
            "Delete Exercise",
            $"Are you sure you want to delete {exercise.name}?",
            "Yes", "No"
        );

        if (!confirm)
        {
            return;
        }
        
        exercise.ExerciseSets.Clear();

        routineVm.NewRoutine.Exercises.Remove(exercise);
    }
    
    [RelayCommand]
    public async Task SaveSelectedExercises()
    {
        if (!SelectedExercises.Any())
        {
            return;
        }

        if (routineVm.NewRoutine == null || routineVm.NewRoutine.Id != 0)
        {
            return;
        }

        foreach (var exercise in SelectedExercises)
        {
            if (!routineVm.NewRoutine.Exercises.Any(e => e.name == exercise.name))
            {
                exercise.RoutineId = routineVm.NewRoutine.Id;
                exercise.ExerciseSets = new ObservableCollection<ExerciseSets>();

                var defaultSet = new ExerciseSets
                {
                    ExerciseId = exercise.Id,
                    SetNumber = 1,
                    Previous = "-",
                    Weight = 0,
                    Reps = 0
                };

                exercise.ExerciseSets.Add(defaultSet);
                routineVm.NewRoutine.Exercises.Add(exercise);
            }
        }

        await routineVm.RefreshExerciseList();
    }
    
    [RelayCommand]
    public async Task AddUpdatedExercise(Exercises exercise)
    {
        if (routineVm.EditedRoutine == null) return;

        var existingExerciseNames = routineVm.EditedRoutine.Exercises.Select(e => e.name).ToHashSet();
        if (existingExerciseNames.Contains(exercise.name))
        {
            return;
        }

        exercise.RoutineId = routineVm.EditedRoutine.Id;
        exercise.ExerciseSets = new ObservableCollection<ExerciseSets>();

        var lastSet = exercise.Id > 0 
            ? (await dbHelper.GetSetsByExerciseIdAsync(exercise.Id)).LastOrDefault()
            : null;

        var defaultSet = new ExerciseSets
        {
            ExerciseId = exercise.Id,
            SetNumber = lastSet != null ? lastSet.SetNumber + 1 : 1,
            Previous = (lastSet != null && lastSet.Reps > 0)
                ? $"{lastSet.Weight} lbs x {lastSet.Reps}" 
                : "-",
            Weight = 0,
            Reps = 0
        };
        
        exercise.ExerciseSets.Add(defaultSet);

        routineVm.EditedRoutine.Exercises.Add(exercise);
    }

    [RelayCommand]
    public async Task DeleteUpdatedExercise(Exercises exercise)
    {
        if (routineVm.EditedRoutine == null) return;

        bool confirm = await Shell.Current.DisplayAlert(
            "Delete Exercise",
            $"Are you sure you want to delete {exercise.name}?",
            "Yes", "No"
        );

        if (!confirm)
        {
            return;
        }

        if (exercise.Id != 0)
        {
            routineVm.DeletedExercises.Add(exercise);
        }

        exercise.ExerciseSets.Clear();
        routineVm.EditedRoutine.Exercises.Remove(exercise);
    }
}