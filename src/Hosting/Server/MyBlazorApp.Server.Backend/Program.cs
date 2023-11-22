using MyBlazorApp.Services.DiscriminatedUnions.Server.V1;
using MyBlazorApp.Services.WeatherForecasts.Server.V1;

var builder = WebApplication.CreateBuilder(args);

builder.AddServerDefaults();
builder.ConfigureServerOpenTelemetry(new(ServiceName: "Backend"));

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services
    .AddWeatherForecastsService()
    .AddDiscriminatedUnionsService();

var app = builder.Build();

app.MapWeatherForecastsServiceEndpoints();
app.MapDiscriminatedUnionsEndpoints();

app.MapReverseProxy();

app.MapDefaultEndpoints();

app.Run();