using MyBlazorApp.BlazorClient.Backend.Models;
using MyBlazorApp.Server.Shared;
using MyBlazorApp.Services.DiscriminatedUnions.Server;
using MyBlazorApp.Services.WeatherForecasts.Server;
using MyBlazorApp.Utility;

namespace MyBlazorApp.Server.BlazorServer;

public static class Startup
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
           .AddSingleton(Json.CreateDefaultOptions());

        services
           .AddScoped<StateStorage>()
           .AddScoped(_ => new ThemeSwitch(Theme.Red))

           .AddWeatherForecastsService()
           .AddDiscriminatedUnionsService();

        services.AddServerTelemetry(configuration);

        return services;
    }
}