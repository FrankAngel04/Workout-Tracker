using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

public partial class AddWorkoutPage : ContentPage
{
    private readonly RoutineViewModel routineVm;
    private readonly ExerciseViewModel exerciseVm;
    private readonly ExerciseSetViewModel exerciseSetVm;

    public AddWorkoutPage(RoutineViewModel routineVm, ExerciseViewModel exerciseVm, ExerciseSetViewModel exerciseSetVm)
    {
        InitializeComponent();
        this.routineVm = routineVm;
        this.exerciseVm = exerciseVm;
        this.exerciseSetVm = exerciseSetVm;
        
        BindingContext = this.routineVm;
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (routineVm.NewRoutine == null || routineVm.NewRoutine.Id != 0)
        {
            routineVm.NewRoutine = new Routines
            {
                Id = 0,
                Name = string.Empty,
                Exercises = new ObservableCollection<Exercises>()
            };
        }

        MainThread.BeginInvokeOnMainThread(() =>
        {
            routineVm.ExerciseList.Clear();
            foreach (var exercise in routineVm.NewRoutine.Exercises)
            {
                routineVm.ExerciseList.Add(exercise);
            }
        });
    }
    
    protected override bool OnBackButtonPressed()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            bool confirm = await DisplayAlert("Discard Changes?", 
                "Are you sure you want to discard this routine?", "Yes", "No");

            if (!confirm) return;

            if (routineVm.NewRoutine != null && routineVm.NewRoutine.Id == 0)
            {
                routineVm.NewRoutine = null;
            }

            await Navigation.PopAsync();
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
        if (routineVm.NewRoutine == null || routineVm.NewRoutine.Id > 0)
        {
            routineVm.InitializeNewRoutine();
        }

        exerciseVm.Initialize(routineVm.NewRoutine.Id);

        await Navigation.PushModalAsync(new AddExercisePage(routineVm, exerciseVm));
    }




    private async void DeleteExerciseButton_OnClicked(object? sender, EventArgs e)
    {
        if (sender is Button button)
        {
            if (button.BindingContext is Exercises exerciseToDelete)
            {
                await exerciseVm.DeleteExerciseCommand.ExecuteAsync(exerciseToDelete);
            }
        }
    }
    
    private async void AddSetButton_OnClicked(object? sender, EventArgs e)
    {
        if (sender is Button button)
        {
            if (button.BindingContext is Exercises exercise)
            {
                await exerciseSetVm.AddSetCommand.ExecuteAsync(exercise);
            }
        }
    }

    private async void DeleteSetButton_OnClicked(object? sender, EventArgs e)
    {
        if (sender is SwipeItem swipeItem)
        {
            if (swipeItem.BindingContext is ExerciseSets setToDelete)
            {
                await exerciseSetVm.DeleteSetCommand.ExecuteAsync(setToDelete);
            }
        }
    }
    
    private async void CloseButton_OnClicked(object? sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Discard Changes?", 
            "Are you sure you want to discard this routine?", "Yes", "No");

        if (!confirm) return;

        if (routineVm.NewRoutine != null && routineVm.NewRoutine.Id == 0)
        {
            routineVm.NewRoutine = null;
        }

        await Navigation.PopAsync();
    }
}