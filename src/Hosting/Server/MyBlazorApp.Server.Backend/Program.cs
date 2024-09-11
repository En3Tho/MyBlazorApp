using MyBlazorApp.Services.DiscriminatedUnions.Server.V1;
using MyBlazorApp.Services.WeatherForecasts.Server.V1;

var builder = WebApplication.CreateBuilder(args);

builder.AddServerDefaults();
builder.ConfigureServerOpenTelemetry(new());

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services
    .AddWeatherForecastsService()
    .AddDiscriminatedUnionsService();

builder.Services.AddCors(o =>
    o.AddDefaultPolicy(builder =>
        builder
            .SetIsOriginAllowed(_ => true)
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()));

var app = builder.Build();

app.UseCors();

app.MapWeatherForecastsService();
app.MapDiscriminatedUnionsService();

app.MapReverseProxy();

app.MapDefaultEndpoints();

app.Run();