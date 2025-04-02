using System.Threading.Tasks;
using D424.Helpers;
using D424.Pages;
using D424.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace D424;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        
        var dbHelper = new DatabaseHelper();
        
        Task.Run(() => dbHelper.InitializeDatabaseAsync());

        if (Preferences.Get("IsLoggedIn", false))
        {
            MainPage = new AppShell();
        }
        else
        {
            MainPage = new LoginPage(dbHelper);
        }
    }
}