﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBlazorApp.BlazorClient.Backend.Models;
using MyBlazorApp.Services.DiscriminatedUnions.Clients;
using MyBlazorApp.Services.WeatherForecasts.Clients;
using MyBlazorApp.Utility;

namespace MyBlazorApp.BlazorClient.Maui;

public static class Startup
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
           .AddSingleton(Json.CreateDefaultOptions())
           .AddSingleton<StateStorage>()
           .AddSingleton(new ThemeSwitch(Theme.Red))

           .AddWeatherForecastsHttpClient(configuration)
           .AddDiscriminatedUnionsHttpClient(configuration); // load theme from user config or smth?

        return services;
    }
}