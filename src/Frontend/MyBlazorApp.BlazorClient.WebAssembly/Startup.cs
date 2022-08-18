using System;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using En3Tho.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using MyBlazorApp.BlazorClient.Backend.Models;
using MyBlazorApp.Services.DiscriminatedUnions.Clients;
using MyBlazorApp.Services.WeatherForecasts.Clients;
using MyBlazorApp.Utility;

namespace MyBlazorApp.BlazorClient.WebAssembly;

public static class Startup
{
    public static WebAssemblyHostBuilder ConfigureOptions(this WebAssemblyHostBuilder builder)
    {
        Console.WriteLine(builder.Configuration.Build().GetDebugView());
        return builder;
    }

    public static WebAssemblyHostBuilder ConfigureLogging(this WebAssemblyHostBuilder builder)
    {
        builder.Logging.Configure(options =>
            options.ActivityTrackingOptions = ActivityTrackingOptions.ParentId
                                            | ActivityTrackingOptions.SpanId
                                            | ActivityTrackingOptions.TraceId);

        return builder;
    }

    public static WebAssemblyHostBuilder AddServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services
               .AddSingleton(Json.CreateDefaultOptions())
               .AddSingleton<StateStorage>()
               .AddSingleton(new ThemeSwitch(Theme.Red))
               .AddWeatherForecastsHttpClient(builder.Configuration)
               .AddDiscriminatedUnionsHttpClient(builder.Configuration);

        return builder;
    }
}