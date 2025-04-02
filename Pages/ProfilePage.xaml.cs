using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using D424.Helpers;
using D424.ViewModels;

namespace D424.Pages;

public partial class ProfilePage : ContentPage
{
    private readonly DatabaseHelper _dbHelper;
    private readonly ProfileViewModel _profileVm;

    public ProfilePage(DatabaseHelper dbHelper, ProfileViewModel profileVm)
    {
        InitializeComponent();
        _dbHelper = dbHelper;
        _profileVm = profileVm;
        BindingContext = _profileVm;
        
        MessagingCenter.Subscribe<LoginPage>(this, "UserLoggedIn", (sender) =>
        {
            _profileVm.LoadUserProfile();
        });

        MessagingCenter.Subscribe<EditProfilePage>(this, "ProfileUpdated", (sender) =>
        {
            _profileVm.LoadUserProfile();
        });
    }

    private async void OnEditProfileClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditProfilePage(_dbHelper));
    }

    private async void OnDeleteAccountClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert(
            "Delete Account", 
            "Are you sure you want to delete your account? This action cannot be undone.", 
            "Yes", "No"
        );

        if (!confirm) return;

        int userId = Preferences.Get("UserId", 0);
        if (userId == 0)
        {
            await DisplayAlert("Error", "User ID not found.", "OK");
            return;
        }

        await _dbHelper.DeleteUserAccountAsync(userId);

        Preferences.Clear();

        await DisplayAlert("Success", "Your account has been deleted.", "OK");

        Application.Current.MainPage = new LoginPage(_dbHelper);
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Logout", "Are you sure you want to log out?", "Yes", "No");
        if (!confirm) return;
        
        Preferences.Clear();
        
        Preferences.Remove("IsLoggedIn");
        Preferences.Remove("UserId");
        Preferences.Remove("Username");
        Preferences.Remove("Email");
        Preferences.Remove("AccountCreated");
        Preferences.Remove("PasswordUpdated");
        
        Application.Current.MainPage = new LoginPage(_dbHelper);
    }
}