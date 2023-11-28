using Microsoft.Extensions.Hosting;
using MyBlazorApp.Client.WebAssembly;

var builder = new WebAssemblyHostApplicationBuilder(args);

builder.AddWasmEnvironmentVariables();
builder.Configuration.AddEnvironmentVariables();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

builder.AddClientDefaults();
// turn off for now?
builder.ConfigureOpenTelemetry(new (Metrics: false, Logging: false, Traces: false));

var host = builder.Build();

var hostedServices = host.Services.GetRequiredService<IEnumerable<IHostedService>>().ToArray();
var logger = host.Services.GetRequiredService<ILogger<Program>>();

try
{
    foreach (var hostedService in hostedServices)
    {
        logger.LogInformation("Starting service {ServiceName}", hostedService.GetType().Name);
        await hostedService.StartAsync(CancellationToken.None);
    }

    await host.RunAsync();
}
catch (Exception e)
{
    logger.LogError("Unhandled exception: {Exception}", e);
}
finally
{
    foreach (var hostedService in hostedServices)
    {
        await hostedService.StopAsync(CancellationToken.None);
    }
}