using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace D424.Classes;

public class ExerciseSetBase : ObservableObject
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SetNumber { get; set; }
    public string Previous { get; set; }
    private double _weight;
    private int _reps;

    public double Weight
    {
        get => _weight;
        set
        {
            if (SetProperty(ref _weight, value))
            {
                Debug.WriteLine($"Weight updated to {value}");
            }
        }
    }

    public int Reps
    {
        get => _reps;
        set
        {
            if (SetProperty(ref _reps, value))
            {
                Debug.WriteLine($"Reps updated to {value}");
            }
        }
    }

    
    public virtual async Task<int> SaveToDatabase(SQLiteAsyncConnection database)
    {
        if (Weight < 0)
        {
            Debug.WriteLine("ERROR: Weight cannot be negative.");
            return 0;
        }

        if (Id == 0)
        {
            await database.InsertAsync(this);
            Debug.WriteLine($"INSERTED Set {SetNumber} (ID: {Id}) with Weight={Weight}, Reps={Reps}");
        }
        else
        {
            await database.UpdateAsync(this);
            Debug.WriteLine($"UPDATED Set {SetNumber} (ID: {Id}) with Weight={Weight}, Reps={Reps}");
        }

        return Id;
    }

}