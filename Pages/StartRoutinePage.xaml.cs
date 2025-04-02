using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D424.Classes;
using D424.ViewModels;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;

namespace D424.Pages;

public partial class StartRoutinePage : ContentPage
{
    public StartRoutinePage(RoutineViewModel routineVm)
    {
        InitializeComponent();
        BindingContext = routineVm;
    }

    private void OnSetCompleted(object? sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkBox && checkBox.BindingContext is ExerciseSets set)
        {
            set.IsCompleted = e.Value;
            CheckIfAllSetsCompleted();
        }
    }
    
    private void CheckIfAllSetsCompleted()
    {
        if (BindingContext is RoutineViewModel routineVm)
        {
            bool allCompleted = routineVm.ExerciseList
                .SelectMany(exercise => exercise.ExerciseSets)
                .All(set => set.IsCompleted);

            SaveButton.IsEnabled = allCompleted;
        }
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

    private void CloseButton_OnClicked(object? sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}