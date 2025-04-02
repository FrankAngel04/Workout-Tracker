using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D424.ViewModels;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;

namespace D424.Pages;

public partial class TimerPage : ContentPage
{
    public TimerPage()
    {
        InitializeComponent();
        BindingContext = new TimerViewModel();
    }
}