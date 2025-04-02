using System.Diagnostics;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using D424.Helpers;
using D424.Pages;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace D424.ViewModels;

public partial class ProfileViewModel : ObservableObject
{
    public ProfileViewModel()
    {
        LoadUserProfile();
        
        MessagingCenter.Subscribe<LoginPage>(this, "UserLoggedIn", (sender) =>
        {
            LoadUserProfile();
        });
    }
    
    [ObservableProperty]
    private string _username;

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _accountCreated;

    [ObservableProperty]
    private string _profileImage;
    
    public void LoadUserProfile()
    {
        var savedUsername = Preferences.Get("Username", "Guest");

        Username = string.IsNullOrWhiteSpace(savedUsername) 
            ? "Guest" 
            : char.ToUpper(savedUsername[0]) + savedUsername.Substring(1).ToLower();

        Email = Preferences.Get("Email", "No Email");
        AccountCreated = $"Joined: {Preferences.Get("AccountCreated", "Unknown")}";
        ProfileImage = Preferences.Get("ProfileImage", "default_profile.png");

        OnPropertyChanged(nameof(Username));
        OnPropertyChanged(nameof(Email));
        OnPropertyChanged(nameof(AccountCreated));
    }
}