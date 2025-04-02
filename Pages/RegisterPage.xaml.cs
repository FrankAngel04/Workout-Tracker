using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using D424.Helpers;
using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace D424.Pages;

public partial class RegisterPage : ContentPage
{
    private readonly DatabaseHelper _dbHelper;

    public RegisterPage(DatabaseHelper dbHelper)
    {
        InitializeComponent();
        _dbHelper = dbHelper;
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
    
    private void OnPasswordFocused(object sender, FocusEventArgs e)
    {
        OnEntryFocused(sender, e);
        PasswordRequirements.IsVisible = true;
    }


    private void OnPasswordTextChanged(object sender, TextChangedEventArgs e)
    {
        ValidatePassword(e.NewTextValue);
    }

    private void ValidatePassword(string password)
    {
        bool hasUpper = password.Any(char.IsUpper);
        bool hasLower = password.Any(char.IsLower);
        bool hasNumber = password.Any(char.IsDigit);

        UppercaseRequirement.TextColor = hasUpper ? Colors.LimeGreen : Colors.Red;
        LowercaseRequirement.TextColor = hasLower ? Colors.LimeGreen : Colors.Red;
        NumberRequirement.TextColor = hasNumber ? Colors.LimeGreen : Colors.Red;
    }
    
    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text?.Trim().ToLower();
        string password = PasswordEntry.Text;
        string confirmPassword = ConfirmPasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
        {
            ShowError("Please fill in all fields.");
            return;
        }
        
        if (!Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).+$"))
        {
            ShowError("Password must contain at least one uppercase, one lowercase, and one number.");
            return;
        }

        if (password != confirmPassword)
        {
            ShowError("Passwords do not match.");
            return;
        }

        var registerButton = (Button)sender;
        registerButton.Text = "Registering...";
        registerButton.IsEnabled = false;

        bool success = await _dbHelper.RegisterUserAsync(username, password);

        registerButton.Text = "Register";
        registerButton.IsEnabled = true;

        if (success)
        {
            await DisplayAlert("Success", "Your account has been created!", "OK");
            await Navigation.PopModalAsync();
        }
        else
        {
            ShowError("Username already exists. Try another.");
        }
    }

    private void ShowError(string message)
    {
        ErrorLabel.Text = message;
        ErrorLabel.IsVisible = true;
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}