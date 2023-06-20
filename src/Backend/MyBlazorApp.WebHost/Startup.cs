using System.Text.Json;
using En3Tho.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBlazorApp.Services.DiscriminatedUnions.Hosting;
using MyBlazorApp.Services.WeatherForecasts.Hosting;
using MyBlazorApp.Utility;

namespace MyBlazorApp.WebHost;

public static class Startup
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization();
        services.AddCors(o =>
            o.AddDefaultPolicy(builder =>
                builder
                   .SetIsOriginAllowed(_ => true)
                   .AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod()));

        services.AddWeatherForecastsService()
                .AddDiscriminatedUnionsService();

        services.ConfigureHttpJsonOptions(options =>
            Json.AddFSharpConverters(options.SerializerOptions));

        services.AddOrReplaceSingleton<JsonSerializerOptions>(serviceProvider =>
            serviceProvider.GetRequiredService<JsonOptions>().SerializerOptions);

        return services;
    }
}