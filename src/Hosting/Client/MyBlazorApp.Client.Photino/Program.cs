using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBlazorApp.ComponentsAndPages.Shared;

namespace MyBlazorApp.Client.Photino;

public class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        var builder = new PhotinoHostApplicationBuilder(args);

        builder.AddClientDefaults();
        builder.ConfigureClientOpenTelemetry(new(ServiceName: "Photino"));

        builder.Configuration.AddJsonFile("wwwroot/appsettings.json"); // check if added automatically?

        builder.RootComponents.Add<App>("app");

        var (host, app) = builder.Build();

        app.MainWindow.SetLogVerbosity(0);

        var hostedServices = app.Services.GetRequiredService<IEnumerable<IHostedService>>().ToArray();
        var cts = new CancellationTokenSource();
        app.MainWindow.WindowClosing += (_, _) =>
        {
            cts.Cancel();
            return false; // or true?
        };

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
                hostedService.StopAsync(CancellationToken.None).GetAwaiter().GetResult();
            }
        }
    }
}