using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;
using SQLite;

namespace D424.Classes;

public class Reports
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoutineId { get; set; }
    public string RoutineName { get; set; }
    public string WorkoutDuration { get; set; }
    public DateTime CompletedOn { get; set; }
    public string CompletedOnDisplay => CompletedOn.ToString("ddd, MMM d, yy h:mm tt");
    
    [Ignore]
    public ObservableCollection<CompletedExercises> Exercises { get; set; } = new();
    
    [Ignore]
    public ObservableCollection<CompletedExercises> LeftColumnExercises { get; set; } = new();

    [Ignore]
    public ObservableCollection<CompletedExercises> RightColumnExercises { get; set; } = new();
}