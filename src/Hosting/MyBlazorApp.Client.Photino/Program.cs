using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyBlazorApp.Client.Shared;
using MyBlazorApp.ComponentsAndPages.Shared;
using Photino.Blazor;

namespace MyBlazorApp.Client.Photino;

record LoggingBuilder(IServiceCollection Services) : ILoggingBuilder;

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
        app.Run();
    }
}