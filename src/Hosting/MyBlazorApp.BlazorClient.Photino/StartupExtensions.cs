using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBlazorApp.BlazorClient.Backend.Models;
using MyBlazorApp.Services.DiscriminatedUnions.Clients;
using MyBlazorApp.Services.WeatherForecasts.Clients;
using MyBlazorApp.Utility;
using Photino.Blazor;

namespace MyBlazorApp.BlazorClient.Photino;

public static class StartupExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        services
           .AddSingleton(Json.CreateDefaultOptions())
           .AddSingleton<StateStorage>()
           .AddSingleton(new ThemeSwitch(Theme.Red)) // load theme from user config or smth?
           .AddWeatherForecastsHttpClient(configurationManager)
           .AddDiscriminatedUnionsHttpClient(configurationManager);

        return services;
    }
}