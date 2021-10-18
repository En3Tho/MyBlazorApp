using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyBlazorApp.BlazorClient.Backend.Models;
using MyBlazorApp.Services.DiscriminatedUnions.Clients;
using MyBlazorApp.Services.DiscriminatedUnions.DependencyInjection;
using MyBlazorApp.Services.WeatherForecasts.Clients;
using MyBlazorApp.Services.WeatherForecasts.Clients.DependencyInjection;
using MyBlazorApp.Utility;

namespace MyBlazorApp.BlazorClient.WebAssembly
{
    public static class StartupExtensions
    {
        public static WebAssemblyHostBuilder ConfigureOptions(this WebAssemblyHostBuilder builder)
        {
            builder.Services.Configure<WeatherForecastsServiceConnectionSettings>(builder.Configuration.GetSection(nameof(WeatherForecastsServiceConnectionSettings)));
            builder.Services.Configure<DiscriminatedUnionsServiceConnectionSettings>(builder.Configuration.GetSection(nameof(DiscriminatedUnionsServiceConnectionSettings)));
            Console.WriteLine(builder.Configuration.Build().GetDebugView());
            return builder;
        }

        public static WebAssemblyHostBuilder ConfigureLogging(this WebAssemblyHostBuilder builder)
        {
            builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
            builder.Logging.Configure(options =>
                options.ActivityTrackingOptions = ActivityTrackingOptions.ParentId
                                                | ActivityTrackingOptions.SpanId
                                                | ActivityTrackingOptions.TraceId);

            return builder;
        }

        private static WebAssemblyHostBuilder ConfigureSingletons(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddSingleton(Json.CreateDefaultOptions());
            builder.Services.AddSingleton<ComponentDataProvider>();
            builder.Services.AddSingleton(new ThemeSwitch(Theme.Red)); // load theme from user config or smth?
            // global StateHasChanged event to simplify programming? MessagePipe ?
            return builder;
        }

        private static WebAssemblyHostBuilder ConfigureScoped(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddWeatherForecastsHttpClient(new Uri(builder.HostEnvironment.BaseAddress));
            builder.Services.AddDiscriminatedUnionsHttpClient(new Uri(builder.HostEnvironment.BaseAddress));
            return builder;
        }

        private static WebAssemblyHostBuilder ConfigureTransient(this WebAssemblyHostBuilder builder)
        {
            return builder;
        }

        public static WebAssemblyHostBuilder ConfigureDI(this WebAssemblyHostBuilder builder) =>
            builder.ConfigureScoped()
                   .ConfigureSingletons()
                   .ConfigureTransient();
    }
}