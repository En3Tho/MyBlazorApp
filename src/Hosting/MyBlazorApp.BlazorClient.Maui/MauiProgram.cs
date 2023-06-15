using Microsoft.Extensions.Configuration;

namespace MyBlazorApp.BlazorClient.Maui;

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
            });
        builder.Configuration.AddJsonFile("wwwroot/appsettings.json");

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddServices(builder.Configuration);

        return builder.Build();
    }
}