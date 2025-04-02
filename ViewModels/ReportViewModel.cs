using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using D424.Classes;
using D424.Helpers;
using D424.Pages;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace D424.ViewModels;

public partial class ReportViewModel : ObservableObject
{
    private readonly DatabaseHelper dbHelper;

    [ObservableProperty]
    private ObservableCollection<Reports> completedWorkouts = [];
    
    [ObservableProperty]
    private string searchText = "";

    [ObservableProperty]
    private DateTime? startDate;

    [ObservableProperty]
    private DateTime? endDate;
    
    public ReportViewModel(DatabaseHelper dbHelper)
    {
        this.dbHelper = dbHelper;
        
        StartDate = DateTime.Today;
        EndDate = DateTime.Today;
        
        MessagingCenter.Subscribe<LoginPage>(this, "ReportsUpdated", async (sender) =>
        {
            await LoadCompletedWorkouts();
        });
        
        Task.Run(LoadCompletedWorkouts);
    }

    partial void OnStartDateChanged(DateTime? value)
    {
        if (value > EndDate) 
            EndDate = value;
    }

    partial void OnEndDateChanged(DateTime? value)
    {
        if (value < StartDate) 
            StartDate = value;
    }
    
    public async void RefreshReports()
    {
        var workouts = await dbHelper.GetWorkoutReportsAsync();
        
        var sortedWorkouts = workouts.OrderByDescending(w => w.CompletedOn).ToList();

        CompletedWorkouts.Clear();

        foreach (var workout in sortedWorkouts)
        {
            var completedExercises = await dbHelper.GetCompletedExercisesAsync(workout.Id);

            foreach (var exercise in completedExercises)
            {
                var completedSets = await dbHelper.GetCompletedSetsAsync(exercise.Id);
                exercise.ExerciseSets = new ObservableCollection<CompletedSets>(completedSets);
            }

            workout.Exercises = new ObservableCollection<CompletedExercises>(completedExercises);

            int splitIndex = (completedExercises.Count + 1) / 2;
            workout.LeftColumnExercises = new ObservableCollection<CompletedExercises>(completedExercises.Take(splitIndex));
            workout.RightColumnExercises = new ObservableCollection<CompletedExercises>(completedExercises.Skip(splitIndex));

            CompletedWorkouts.Add(workout);
        }

        OnPropertyChanged(nameof(CompletedWorkouts));
    }

    private async Task LoadCompletedWorkouts()
    {
        int userId = Preferences.Get("UserId", 0);
        var workouts = await dbHelper.GetWorkoutReportsAsync();

        var filteredWorkouts = workouts.Where(w => w.UserId == userId)
            .OrderByDescending(w => w.CompletedOn)
            .ToList();

        CompletedWorkouts.Clear();

        foreach (var workout in filteredWorkouts)
        {
            var completedExercises = await dbHelper.GetCompletedExercisesAsync(workout.Id);

            foreach (var exercise in completedExercises)
            {
                var completedSets = await dbHelper.GetCompletedSetsAsync(exercise.Id);
                exercise.ExerciseSets = new ObservableCollection<CompletedSets>(completedSets);
            }

            workout.Exercises = new ObservableCollection<CompletedExercises>(completedExercises);

            int splitIndex = (completedExercises.Count + 1) / 2;
            workout.LeftColumnExercises = new ObservableCollection<CompletedExercises>(completedExercises.Take(splitIndex));
            workout.RightColumnExercises = new ObservableCollection<CompletedExercises>(completedExercises.Skip(splitIndex));

            CompletedWorkouts.Add(workout);
        }

        OnPropertyChanged(nameof(CompletedWorkouts));
    }

    
    [RelayCommand]
    private async Task DeleteWorkoutReport(Reports workout)
    {
        if (workout == null) return;

        bool confirm = await Shell.Current.DisplayAlert(
            "Delete Workout", 
            $"Are you sure you want to delete {workout.RoutineName}?", 
            "Yes", "No"
        );

        if (!confirm) return;

        await dbHelper.DeleteWorkoutReportAsync(workout);
        CompletedWorkouts.Remove(workout);
    }
    
    [RelayCommand]
    public async Task SearchReports()
    {
        var allReports = await dbHelper.GetWorkoutReportsAsync();

        var filteredReports = allReports
            .Where(r =>
                (string.IsNullOrEmpty(SearchText) || r.RoutineName.Contains(SearchText, StringComparison.OrdinalIgnoreCase)) &&
                (!StartDate.HasValue || r.CompletedOn.Date >= StartDate.Value.Date) &&
                (!EndDate.HasValue || r.CompletedOn.Date <= EndDate.Value.Date))
            .OrderByDescending(r => r.CompletedOn)
            .ToList();

        CompletedWorkouts.Clear();

        foreach (var workout in filteredReports)
        {
            var completedExercises = await dbHelper.GetCompletedExercisesAsync(workout.Id);

            foreach (var exercise in completedExercises)
            {
                var completedSets = await dbHelper.GetCompletedSetsAsync(exercise.Id);
                exercise.ExerciseSets = new ObservableCollection<CompletedSets>(completedSets);
            }

            workout.Exercises = new ObservableCollection<CompletedExercises>(completedExercises);

            int splitIndex = (completedExercises.Count + 1) / 2;
            workout.LeftColumnExercises = new ObservableCollection<CompletedExercises>(completedExercises.Take(splitIndex));
            workout.RightColumnExercises = new ObservableCollection<CompletedExercises>(completedExercises.Skip(splitIndex));

            CompletedWorkouts.Add(workout);
        }

        OnPropertyChanged(nameof(CompletedWorkouts));
    }
}