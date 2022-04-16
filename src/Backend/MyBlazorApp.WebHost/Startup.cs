using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBlazorApp.Services.DiscriminatedUnions.DependencyInjection;
using MyBlazorApp.Services.WeatherForecasts.Hosting.DependencyInjection;
using MyBlazorApp.Utility;

namespace MyBlazorApp.WebHost
{
    public static class Startup
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews()
                .AddJsonOptions(options => Json.UpdateExistingOptions(options.JsonSerializerOptions));
            services.AddCors(o =>
                o.AddDefaultPolicy(builder =>
                    builder
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()));

            services.AddWeatherForecastsService()
                    .AddDiscriminatedUnionsService();
            services.AddRazorPages();
            services.AddSingleton(Json.CreateDefaultOptions());
            return services;
        }
    }
}
