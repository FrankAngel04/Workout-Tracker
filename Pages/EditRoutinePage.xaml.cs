using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D424.Classes;
using D424.Helpers;
using D424.ViewModels;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;

namespace D424.Pages;

public partial class EditRoutinePage : ContentPage
{
    private readonly DatabaseHelper dbHelper;
    private readonly RoutineViewModel routineVm;
    private readonly ExerciseViewModel exerciseVm;
    private readonly ExerciseSetViewModel exerciseSetVm;

    public EditRoutinePage(DatabaseHelper dbHelper, RoutineViewModel routineVm, ExerciseViewModel exerciseVm, ExerciseSetViewModel exerciseSetVm)
    {
        InitializeComponent();
        this.dbHelper = dbHelper;
        this.routineVm = routineVm;
        this.exerciseVm = exerciseVm;
        this.exerciseSetVm = exerciseSetVm;
        
        BindingContext = this.routineVm;
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (routineVm.EditedRoutine == null || routineVm.EditedRoutine.Id <= 0)
        {
            await DisplayAlert("Error", "No routine selected.", "OK");
            await Navigation.PopAsync();
            return;
        }

        if (routineVm.EditedRoutine.Exercises.Count == 0)
        {
            await routineVm.LoadRoutineForEditing(routineVm.EditedRoutine);
        }
    }
    
    protected override bool OnBackButtonPressed()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            bool confirm = await DisplayAlert("Discard Changes?", 
                "Are you sure you want to discard all changes?", 
                "Yes", "No");

            if (confirm)
            {
                routineVm.EditedRoutine = routineVm.DeepCopyRoutine(routineVm.OriginalRoutine);

                await Navigation.PopAsync(); }
        });

        return true; 
    }
    
    private void OnEntryFocused(object sender, FocusEventArgs e)
    {
        if (sender is Entry entry && !string.IsNullOrEmpty(entry.Text))
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(100);
                entry.CursorPosition = 0;
                entry.SelectionLength = entry.Text.Length;
            });
        }
    }
    
    private async void AddExerciseButton_OnClicked(object? sender, EventArgs e)
    {
        if (routineVm.EditedRoutine == null || routineVm.EditedRoutine.Id <= 0)
        {
            return;
        }
        
        var exerciseVm = new ExerciseViewModel(dbHelper, new ExerciseHelper(), routineVm);
    
        exerciseVm.Initialize(routineVm.EditedRoutine.Id);

        await Navigation.PushModalAsync(new AddExercisePage(routineVm, exerciseVm));
    }

    private async void DeleteExerciseButton_OnClicked(object? sender, EventArgs e)
    {
        if (sender is Button button)
        {
            if (button.BindingContext is Exercises exerciseToDelete)
            {
                await exerciseVm.DeleteUpdatedExerciseCommand.ExecuteAsync(exerciseToDelete);
            }
        }
    }
    
    private async void AddSetButton_OnClicked(object? sender, EventArgs e)
    {
        if (sender is Button button)
        {
            if (button.BindingContext is Exercises exercise)
            {
                await exerciseSetVm.AddUpdatedSetCommand.ExecuteAsync(exercise);
            }
        }
    }

    private async void DeleteSetButton_OnClicked(object? sender, EventArgs e)
    {
        if (sender is SwipeItem swipeItem)
        {
            if (swipeItem.BindingContext is ExerciseSets setToDelete)
            {
                await exerciseSetVm.DeleteUpdatedSetCommand.ExecuteAsync(setToDelete);
            }
        }
    }
    
    private async void CloseButton_OnClicked(object? sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Discard Changes?", 
            "Are you sure you want to discard all changes?", 
            "Yes", "No");

        if (confirm)
        {
            if (routineVm.OriginalRoutine == null)
            {
                await Navigation.PopAsync();
                return;
            }
        
            routineVm.EditedRoutine = routineVm.DeepCopyRoutine(routineVm.OriginalRoutine);
        
            await Navigation.PopAsync();
        }
    }
}