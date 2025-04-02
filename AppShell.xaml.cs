using Microsoft.Maui.Controls;

namespace D424;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        CurrentItem = MainTabBar.Items[2];
    }
}