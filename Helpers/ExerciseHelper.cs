using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using D424.Classes;
using Microsoft.Maui.Storage;

namespace D424.Helpers;

public class ExerciseHelper
{
    List<Exercises> exerciseList = new ();
    
    public async Task<List<Exercises>> GetExercises()
    {
        try
        {
            if (exerciseList?.Count > 0)
                return exerciseList;

            var stream = await FileSystem.OpenAppPackageFileAsync("exercises.json");
            var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            exerciseList = JsonSerializer.Deserialize<List<Exercises>>(contents) ?? new List<Exercises>();

            Debug.WriteLine($"Loaded {exerciseList.Count} exercises from JSON.");
            return exerciseList;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading exercises: {ex.Message}");
            return new List<Exercises>();
        }
    }
}