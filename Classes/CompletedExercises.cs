using System.Collections.ObjectModel;
using SQLite;

namespace D424.Classes;

public class CompletedExercises : ExerciseBase
{
    public int ReportId { get; set; }
    
    [Ignore]
    public ObservableCollection<CompletedSets> ExerciseSets { get; internal set; } = new();
}