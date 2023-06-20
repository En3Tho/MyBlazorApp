using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyBlazorApp.Services.DiscriminatedUnions.Hosting;
using MyBlazorApp.Services.WeatherForecasts.Hosting;
using MyBlazorApp.WebHost;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddServices(builder.Configuration);

builder.Logging.ClearProviders();
builder.Logging.Configure(logging =>
{
    logging.ActivityTrackingOptions =
        ActivityTrackingOptions.ParentId
        | ActivityTrackingOptions.SpanId
        | ActivityTrackingOptions.TraceId;
});

var app = builder.Build();

app.MapDiscriminatedUnionsEndpoints();
app.MapWeatherForecastsServiceEndpoints();

app.UseCors();
app.UseAuthorization();
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Run();