using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D424.ViewModels;
using Microsoft.Maui.Controls;

namespace D424.Pages;

public partial class ExercisesPage : ContentPage
{
    public ExercisesPage(ExerciseViewModel exerciseVm)
    {
        InitializeComponent();
        BindingContext = exerciseVm;
    }
}