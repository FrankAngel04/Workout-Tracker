using System.Collections.ObjectModel;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace D424.Classes;

public class Exercises : ExerciseBase
{
    public int RoutineId { get; set; }

    [Ignore]
    public ObservableCollection<ExerciseSets> ExerciseSets { get; set; } = new();
}