using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using D424.Classes;
using D424.ViewModels;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;

namespace D424.Pages;

public partial class AddExercisePage : ContentPage
{
    private readonly RoutineViewModel routineVm;
    private readonly ExerciseViewModel exerciseVm;
    
    public AddExercisePage(RoutineViewModel routineVm, ExerciseViewModel exerciseVm)
    {
        InitializeComponent();
        this.routineVm = routineVm;
        this.exerciseVm = exerciseVm;
        
        if (routineVm.EditedRoutine == null)
        {
            routineVm.EditedRoutine = new Routines { Name = "New Routine" };
        }

        exerciseVm.SelectedExercises.Clear();
        BindingContext = this.exerciseVm;
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
    
    private async void SaveButton_OnClicked(object? sender, EventArgs e)
    {
        if (!exerciseVm.SelectedExercises.Any())
        {
            await DisplayAlert("Error", "Please select at least one exercise.", "OK");
            return;
        }

        if (routineVm.NewRoutine != null && routineVm.NewRoutine.Id == 0)
        {
            await exerciseVm.SaveSelectedExercises();
        }
        else
        {
            foreach (var exercise in exerciseVm.SelectedExercises)
            { 
                exerciseVm.AddUpdatedExercise(exercise);
            }
        }

        await Navigation.PopModalAsync(true);
    }

    private async void CloseButton_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync(true);
    }

    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        foreach (var addedItem in e.CurrentSelection.Except(e.PreviousSelection))
        {
            if (addedItem is Exercises exercise && !exerciseVm.SelectedExercises.Contains(exercise))
            {
                exerciseVm.SelectedExercises.Add(exercise);
            }
        }

        foreach (var removedItem in e.PreviousSelection.Except(e.CurrentSelection))
        {
            if (removedItem is Exercises exercise && exerciseVm.SelectedExercises.Contains(exercise))
            {
                exerciseVm.SelectedExercises.Remove(exercise);
            }
        }
    }
}