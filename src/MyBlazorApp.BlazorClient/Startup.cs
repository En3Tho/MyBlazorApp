using System;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyBlazorApp.Api.HttpClients;
using MyBlazorApp.BlazorClient.Backend.Models;

namespace MyBlazorApp.BlazorClient
{
    public static class Startup
    {
        public static WebAssemblyHostBuilder ConfigureOptions(this WebAssemblyHostBuilder builder)
        {
            Console.WriteLine(builder.Configuration.Build().GetDebugView());
            return builder;
        }

        public static WebAssemblyHostBuilder ConfigureLogging(this WebAssemblyHostBuilder builder)
        {
            builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
            return builder;
        }

        private static WebAssemblyHostBuilder ConfigureSingletons(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddSingleton(new JsonSerializerOptions { PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            builder.Services.AddSingleton<ComponentDataProvider>();
            return builder;
        }

        private static WebAssemblyHostBuilder ConfigureScoped(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<WeatherForecastApiV1HttpClient>();
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