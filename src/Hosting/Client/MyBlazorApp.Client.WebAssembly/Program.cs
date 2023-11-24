using System.Text.Json;
using Microsoft.Extensions.Hosting;
using OpenTelemetry;
using OpenTelemetry.Exporter;

var builder = new WebAssemblyHostApplicationBuilder(args);

var httpClient = new HttpClient { BaseAddress = new(builder.HostEnvironment.BaseAddress) };
var wasmEnv = await httpClient.GetStringAsync("/wasm/env.json");
var jsonObj = JsonDocument.Parse(wasmEnv);

foreach (var element in jsonObj.RootElement.EnumerateObject())
{
    Environment.SetEnvironmentVariable(element.Name, element.Value.GetString()!);
}

builder.Configuration.AddEnvironmentVariables();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

builder.AddClientDefaults();
builder.ConfigureOpenTelemetry(new (Logging: false, Metrics: false, Protocol: OtlpExportProtocol.HttpProtobuf, ExportProcessorType: ExportProcessorType.Simple));

var host = builder.Build();

var hostedServices = host.Services.GetRequiredService<IEnumerable<IHostedService>>().ToArray();
var logger = host.Services.GetRequiredService<ILogger<Program>>();

foreach (var hostedService in hostedServices)
{
    logger.LogInformation("Starting service {ServiceName}", hostedService.GetType().Name);
    await hostedService.StartAsync(CancellationToken.None);
}

try
{
    await host.RunAsync();
}
finally
{
    foreach (var hostedService in hostedServices)
    {
        await hostedService.StopAsync(CancellationToken.None);
    }
}