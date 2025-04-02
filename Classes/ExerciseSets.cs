using System.ComponentModel.DataAnnotations.Schema;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace D424.Classes;

public partial class ExerciseSets : ExerciseSetBase
{
    public int ExerciseId { get; set; }
    
    [ObservableProperty]
    private bool isCompleted;
}