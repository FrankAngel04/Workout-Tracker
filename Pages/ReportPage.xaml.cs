using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D424.Classes;
using D424.Helpers;
using D424.ViewModels;
using Microsoft.Maui.Controls;

namespace D424.Pages;

public partial class ReportPage : ContentPage
{
    public ReportPage(ReportViewModel reportVm)
    {
        InitializeComponent();
        BindingContext = reportVm;
    }
}

