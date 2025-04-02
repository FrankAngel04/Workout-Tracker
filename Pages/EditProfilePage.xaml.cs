using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using D424.Helpers;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Storage;

namespace D424.Pages;

public partial class EditProfilePage : ContentPage
{
    private readonly DatabaseHelper _dbHelper;

    public EditProfilePage(DatabaseHelper dbHelper)
    {
        InitializeComponent();
        _dbHelper = dbHelper;
        LoadUserProfile();
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
    
    private void OnNewPasswordFocused(object sender, FocusEventArgs e)
    {
        OnEntryFocused(sender, e);
        PasswordRequirements.IsVisible = true;
        ErrorLabel.IsVisible = false;
    }

    private void OnPasswordTextChanged(object sender, TextChangedEventArgs e)
    {
        string password = e.NewTextValue ?? "";

        bool hasUppercase = password.Any(char.IsUpper);
        bool hasLowercase = password.Any(char.IsLower);
        bool hasNumber = password.Any(char.IsDigit);

        UppercaseRequirement.TextColor = hasUppercase ? Colors.LimeGreen : Colors.Red;
        LowercaseRequirement.TextColor = hasLowercase ? Colors.LimeGreen : Colors.Red;
        NumberRequirement.TextColor = hasNumber ? Colors.LimeGreen : Colors.Red;
    }
    
    private void LoadUserProfile()
    {
        NewUsernameEntry.Text = Preferences.Get("Username", "Guest");
    }
    
    private async void OnSaveChangesClicked(object sender, EventArgs e)
    {
        int userId = Preferences.Get("UserId", 0);
        string newUsername = NewUsernameEntry.Text?.Trim();
        string currentPassword = CurrentPasswordEntry.Text;
        string newPassword = NewPasswordEntry.Text;
        string confirmPassword = ConfirmPasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(newUsername) || string.IsNullOrWhiteSpace(currentPassword))
        {
            ShowError("Username and current password are required.");
            return;
        }

        ShowLoading(true);

        var user = await _dbHelper.GetUserByIdAsync(userId);
        if (user == null || !BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash))
        {
            ShowError("Incorrect current password.");
            ShowLoading(false);
            return;
        }

        if (!string.Equals(user.Username, newUsername, StringComparison.OrdinalIgnoreCase))
        {
            var existingUser = await _dbHelper.GetUserByUsernameAsync(newUsername);
            if (existingUser != null)
            {
                ShowError("Username already taken.");
                ShowLoading(false);
                return;
            }
            
            user.Username = newUsername;
            await _dbHelper.UpdateUserAsync(user);
            Preferences.Set("Username", newUsername.ToLower());
        }

        if (!string.IsNullOrWhiteSpace(newPassword))
        {
            if (!Regex.IsMatch(newPassword, @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).+$"))
            {
                ShowError("Password must contain at least one uppercase, one lowercase, and one number.");
                ShowLoading(false);
                return;
            }

            if (newPassword != confirmPassword)
            {
                ShowError("Passwords do not match.");
                ShowLoading(false);
                return;
            }
            
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _dbHelper.UpdateUserAsync(user);
            
            await _dbHelper.GetUserByIdAsync(user.Id);
            
            Preferences.Set("PasswordUpdated", "true");
        }

        MessagingCenter.Send(this, "ProfileUpdated");

        await DisplayAlert("Success", "Profile updated successfully!", "OK");

        ShowLoading(false);
        await Navigation.PopAsync();
    }







    private void ShowError(string message)
    {
        ErrorLabel.Text = message;
        ErrorLabel.IsVisible = true;
    }

    private void ShowLoading(bool isLoading)
    {
        SaveChangesButton.IsEnabled = !isLoading;
        SaveChangesButton.Text = isLoading ? "Saving..." : "Save Changes";
    }
    
    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

}