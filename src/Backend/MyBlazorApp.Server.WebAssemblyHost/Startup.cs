using System.Text.Json;
using En3Tho.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http.Json;
using MyBlazorApp.Server.Shared;
using MyBlazorApp.Services.DiscriminatedUnions.Server.V1;
using MyBlazorApp.Services.WeatherForecasts.Server.V1;
using MyBlazorApp.Utility;

namespace MyBlazorApp.Server.WebAssemblyHost;

public static class Startup
{
    public static IServiceCollection AddMyBlazorAppServer(this IServiceCollection services, IConfiguration configuration)
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

        services.AddServerTelemetry(configuration);

        return services;
    }
}