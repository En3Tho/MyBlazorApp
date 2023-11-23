using System.Text.Json;
using En3Tho.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MyBlazorApp.BlazorClient.Backend.Models;
using MyBlazorApp.Services.DiscriminatedUnions.Client.V1;
using MyBlazorApp.Services.WeatherForecasts.Client.V1;
using MyBlazorApp.Utility;

namespace Microsoft.Extensions.Hosting;

public static class Extensions
{
    public static IHostApplicationBuilder AddClientDefaults(this IHostApplicationBuilder builder)
    {
        builder.Services
            .TryAddSingleton(new JsonSerializerOptions());

        builder.Services
            .Update<JsonSerializerOptions>(Json.AddFSharpConverters)
            .AddSingleton<StateStorage>()
            .AddSingleton(new ThemeSwitch(Theme.Red))

            .AddWeatherForecastsHttpClient()
            .AddDiscriminatedUnionsHttpClient();

        return builder.AddServiceDefaults();
    }
}