using BeFit.Services;
using BeFit.ViewModels;
using BeFit.Views;
using Microsoft.Extensions.Logging;

namespace BeFit;

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

        builder.Services.AddSingleton<DatabaseService>();

        builder.Services.AddTransient<ExerciseViewModel>();
        builder.Services.AddTransient<ExercisePage>();

        builder.Services.AddTransient<SessionViewModel>();
        builder.Services.AddTransient<SessionPage>();

        builder.Services.AddTransient<SessionDetailViewModel>();
        builder.Services.AddTransient<SessionDetailPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}