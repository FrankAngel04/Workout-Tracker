using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace D424.Classes;

public class Routines : ObservableObject
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int UserId { get; set; }
    private string _name;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value?.Trim());
    }
        
    [Ignore]
    public ObservableCollection<Exercises> Exercises { get;  set; } = new();
}