using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using D424.Helpers;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Storage;

namespace D424.Pages;

public partial class LoginPage : ContentPage
{
    private readonly DatabaseHelper _dbHelper;
    private CancellationTokenSource _cts;

    public LoginPage(DatabaseHelper dbHelper)
    {
        InitializeComponent();
        _dbHelper = dbHelper;
    }

    private int _lockoutSeconds = 0;

    private string FormatTime(int totalSeconds) =>
        $"{totalSeconds / 60:D2}:{totalSeconds % 60:D2}";

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
    
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text?.Trim().ToLower();;
        string password = PasswordEntry.Text;
        
        ErrorLabel.IsVisible = false;
        ErrorLabel.Text = "";

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            ShowError("Please enter a valid username and password.");
            return;
        }

        var loginButton = (Button)sender;
        string originalText = loginButton.Text;
        loginButton.Text = "Logging in...";
        loginButton.IsEnabled = false;

        try
        {
            var updatedUser = await _dbHelper.GetUserByIdAsync(Preferences.Get("UserId", 0));
            string actualUsername = updatedUser?.Username ?? username;
            
            var userExists = await _dbHelper.GetUserByUsernameAsync(actualUsername);

            if (userExists == null)
            {
                ShowError("User not found. Please check your username or register an account.");
                loginButton.Text = "Login";
                loginButton.IsEnabled = true;
                return;
            }
            
            var (user, lockoutTime, remainingAttempts) = await Task.Run(() => _dbHelper.LoginUserAsync(username, password));

            loginButton.Text = originalText;
            loginButton.IsEnabled = true;

            if (user == null)
            {
                if (lockoutTime > 0)
                {
                    _lockoutSeconds = lockoutTime;
                    LockoutLabel.Text = $"Try again in {FormatTime(_lockoutSeconds)}";
                    LockoutLabel.IsVisible = true;
                    TriesLabel.IsVisible = false;
                    await LockoutLabel.FadeTo(1, 500);
                    StartCountdown();
                }
                else
                {
                    await InvalidLoginFeedback(remainingAttempts);
                }
                return;
            }

            _cts?.Cancel();
            LockoutLabel.IsVisible = false;
            TriesLabel.IsVisible = false;
            Preferences.Set("IsLoggedIn", true);
            Preferences.Set("UserId", user.Id);
            Preferences.Set("Username", user.Username.ToLower());
            Preferences.Set("Email", user.Email);
            Preferences.Set("AccountCreated", user.AccountCreated.ToString("MMM dd, yyyy"));
            
            int userId = Preferences.Get("UserId", 0);
            Debug.WriteLine($"Logged-in User ID: {userId}");

            var routines = await _dbHelper.GetRoutinesAsync();
            Debug.WriteLine($"Total Routines Found: {routines.Count}");

            MessagingCenter.Send(this, "UserLoggedIn");
            MessagingCenter.Send(this, "RoutinesUpdated");
            MessagingCenter.Send(this, "ReportsUpdated");
            
            Application.Current.MainPage = new AppShell();
        }
        catch (Exception ex)
        {
            ShowError("An error occurred while logging in. Please try again.");
            loginButton.Text = originalText;
            loginButton.IsEnabled = true;
        }
    }

    private void ShowError(string message)
    {
        ErrorLabel.Text = message;
        ErrorLabel.IsVisible = true;
    }

    private async Task InvalidLoginFeedback(int remainingAttempts)
    {
        await UsernameEntry.TranslateTo(-10, 0, 50);
        await UsernameEntry.TranslateTo(10, 0, 50);
        await UsernameEntry.TranslateTo(-10, 0, 50);
        await UsernameEntry.TranslateTo(10, 0, 50);
        await UsernameEntry.TranslateTo(0, 0, 50);

        if (remainingAttempts > 0)
        {
            TriesLabel.Text = $"Incorrect password. {remainingAttempts} attempts left.";
            TriesLabel.TextColor = remainingAttempts == 1 ? Colors.Red : Colors.Orange;
            TriesLabel.IsVisible = true;
        }
        else
        {
            TriesLabel.IsVisible = false;
        }
    }

    private void StartCountdown()
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();

        Device.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            if (_lockoutSeconds > 0)
            {
                _lockoutSeconds--;
                LockoutLabel.Text = $"Try again in {FormatTime(_lockoutSeconds)}";
                return true;
            }

            ResetFailedAttemptsAfterLockout();
            LockoutLabel.IsVisible = false;
            return false;
        });
    }

    private async void ResetFailedAttemptsAfterLockout()
    {
        int userId = Preferences.Get("UserId", 0);
        if (userId == 0) return;

        var user = await _dbHelper.GetUserByIdAsync(userId);
        if (user == null) return;

        user.FailedAttempts = 0;
        user.LastFailedAttempt = null;
        await _dbHelper.UpdateUserAsync(user);
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        UsernameEntry.Text = "";
        PasswordEntry.Text = "";
        await Navigation.PushModalAsync(new RegisterPage(_dbHelper));
    }
}
