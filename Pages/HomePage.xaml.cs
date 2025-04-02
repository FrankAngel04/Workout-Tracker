using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using D424.Classes;
using D424.Helpers;
using D424.ViewModels;
using Microsoft.Maui.Controls;

namespace D424.Pages;

public partial class HomePage : ContentPage
{
    private readonly RoutineViewModel routineVm;
    private readonly ExerciseViewModel exerciseVm;
    private readonly ExerciseSetViewModel exerciseSetVm;
    
    public HomePage(RoutineViewModel routineVm, ExerciseViewModel exerciseVm, ExerciseSetViewModel exerciseSetVm)
    {
        InitializeComponent();
        this.routineVm = routineVm;
        this.exerciseVm = exerciseVm;
        this.exerciseSetVm = exerciseSetVm;
        BindingContext = this.routineVm;
        
    }
    
    private async void AddRoutineButton_OnClicked(object sender, EventArgs e)
    {
        if (routineVm.NewRoutine != null && routineVm.NewRoutine.Id == 0 && routineVm.NewRoutine.Exercises.Count > 0)
        {
            await Shell.Current.Navigation.PushAsync(new AddWorkoutPage(routineVm, exerciseVm, exerciseSetVm));
            return;
        }
        
        routineVm.InitializeNewRoutine();
        
        var newRoutine = new Routines
        {
            Id = 0,
            Name = string.Empty,
            Exercises = new ObservableCollection<Exercises>()
        };

        routineVm.NewRoutine = newRoutine;
    
        await Shell.Current.Navigation.PushAsync(new AddWorkoutPage(routineVm, exerciseVm, exerciseSetVm));
    }
}