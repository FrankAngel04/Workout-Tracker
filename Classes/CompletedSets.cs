using SQLite;

namespace D424.Classes;

public class CompletedSets : ExerciseSetBase
{
    public int CompletedExerciseId { get; set; }
    
    public string DisplaySet => $"{Weight} lbs x {Reps}";
}