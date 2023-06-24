using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyBlazorApp.Client.Shared;
using MyBlazorApp.ComponentsAndPages.Shared;
using MyBlazorApp.Services.WeatherForecasts.Client;
using Photino.Blazor;

namespace MyBlazorApp.Client.Photino;

// TODO: proper host for photino

record LoggingBuilder(IServiceCollection Services) : ILoggingBuilder;

record ForecastsSrv(IWeatherForecastsServiceV1 ForecastsService) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(async () =>
        {
            var forecasts = await ForecastsService.GetForecasts(10);

            foreach (var forecast in forecasts)
            {
                Console.WriteLine(forecast);
            }

        }, cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

public class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        var configurationManager = new ConfigurationManager();
        configurationManager.AddJsonFile("wwwroot/appsettings.json");

        var builder = PhotinoBlazorAppBuilder.CreateDefault(args);

        var loggingBuilder = new LoggingBuilder(builder.Services);
        loggingBuilder.ConfigureLogging(configurationManager);

        builder.Services.AddMyBlazorAppClient(configurationManager);
        builder.Services.AddSingleton<IConfiguration>(_ => configurationManager);

        builder.RootComponents.Add<App>("app");

        var app = builder.Build();
        app.MainWindow.SetLogVerbosity(0);

        var hostedServices = app.Services.GetRequiredService<IEnumerable<IHostedService>>().ToArray();

        // TODO: just write a better photino builder based on consoleapp stuff
        foreach (var hostedService in hostedServices)
        {
            hostedService.StartAsync(CancellationToken.None);
        }
        try
        {
            app.Run();
        }
        finally
        {
            foreach (var hostedService in hostedServices)
            {
                hostedService.StopAsync(CancellationToken.None);
            }
        }
    }
}