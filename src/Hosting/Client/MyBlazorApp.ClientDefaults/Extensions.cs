using Microsoft.Extensions.DependencyInjection;
using MyBlazorApp.BlazorClient.Backend.Models;
using MyBlazorApp.Services.DiscriminatedUnions.Client.V1;
using MyBlazorApp.Services.WeatherForecasts.Client.V1;
using MyBlazorApp.Utility;

namespace Microsoft.Extensions.Hosting;

public static class Extensions
{
    public static IHostApplicationBuilder AddMyBlazorAppClient(this IHostApplicationBuilder builder)
    {
        builder.Services
            .AddSingleton(Json.CreateDefaultOptions())
            .AddSingleton<StateStorage>()
            .AddSingleton(new ThemeSwitch(Theme.Red))

            .AddWeatherForecastsHttpClient()
            .AddDiscriminatedUnionsHttpClient();

        return builder;
    }
}