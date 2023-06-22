using Microsoft.Extensions.Configuration;
using MyBlazorApp.Client.Shared;

namespace MyBlazorApp.Client.Maui;

public static class MauiProgram
{
    public static async Task<MauiApp> CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Logging.ConfigureLogging(builder.Configuration);
        builder.Configuration.ConfigureOptions();
        builder.Configuration.AddJsonStream(await FileSystem.OpenAppPackageFileAsync("wwwroot/appsettings.json"));

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddMyBlazorAppClient(builder.Configuration);

        return builder.Build();
    }
}