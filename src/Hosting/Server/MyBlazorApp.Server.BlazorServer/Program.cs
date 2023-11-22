using MyBlazorApp.BlazorClient.Backend.Models;
using MyBlazorApp.Services.DiscriminatedUnions.Client.V1;
using MyBlazorApp.Services.WeatherForecasts.Client.V1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServerDefaults();
builder.ConfigureServerOpenTelemetry(new(ServiceName: "BlazorServer"));

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services
    .AddScoped<StateStorage>()
    .AddScoped(_ => new ThemeSwitch(Theme.Red))

    .AddWeatherForecastsHttpClient()
    .AddDiscriminatedUnionsHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();