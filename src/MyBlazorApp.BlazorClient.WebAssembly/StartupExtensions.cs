using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyBlazorApp.Api.HttpClients;
using MyBlazorApp.BlazorClient.Backend.Models;
using MyBlazorApp.Utility;

namespace MyBlazorApp.BlazorClient.WebAssembly
{
    public static class StartupExtensions
    {
        public static WebAssemblyHostBuilder ConfigureOptions(this WebAssemblyHostBuilder builder)
        {
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
            // global StateHasChanged event to simplify programming?
            return builder;
        }

        private static WebAssemblyHostBuilder ConfigureScoped(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<WeatherForecastApiV1HttpClient>();
            builder.Services.AddScoped<DiscriminatedUnionApiV1HttpClient>();
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