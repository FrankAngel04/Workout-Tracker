using D424.Helpers;
using D424.Pages;
using D424.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace D424;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<ExerciseHelper>();
        builder.Services.AddSingleton<DatabaseHelper>();
        
        builder.Services.AddSingleton<RoutineViewModel>();
        builder.Services.AddSingleton<ExerciseViewModel>();
        builder.Services.AddSingleton<ExerciseSetViewModel>();
        builder.Services.AddSingleton<ReportViewModel>();
        builder.Services.AddSingleton<ProfileViewModel>();
        builder.Services.AddSingleton<TimerViewModel>();
        
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<RegisterPage>();
        builder.Services.AddTransient<ProfilePage>();
        builder.Services.AddTransient<EditProfilePage>();
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<ExercisesPage>();
        builder.Services.AddTransient<AddWorkoutPage>();
        builder.Services.AddTransient<AddExercisePage>();
        builder.Services.AddTransient<TimerPage>();
        builder.Services.AddTransient<StartRoutinePage>();
        builder.Services.AddTransient<EditRoutinePage>();
        builder.Services.AddTransient<ReportPage>();
        
        return builder.Build();
    }
}