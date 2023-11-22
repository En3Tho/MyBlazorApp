using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyBlazorApp.Client.Shared;
using MyBlazorApp.ComponentsAndPages.Shared;
using Photino.Blazor;

namespace MyBlazorApp.Client.Photino;

// TODO: proper host for photino

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

        var hostedServices = app.Services.GetRequiredService<IEnumerable<IHostedService>>().ToArray();
        var cts = new CancellationTokenSource();
        app.MainWindow.WindowClosing += (_, _) =>
        {
            cts.Cancel();
            return false; // or true?
        };

        // TODO: just write a better photino builder based on consoleapp stuff?
        foreach (var hostedService in hostedServices)
        {
            // can block everything so should be async?
            hostedService.StartAsync(cts.Token).GetAwaiter().GetResult();
        }
        try
        {
            app.Run(); // sync context is initialized here?
        }
        finally
        {
            foreach (var hostedService in hostedServices)
            {
                hostedService.StopAsync(cts.Token).GetAwaiter().GetResult();
            }
        }
    }
}