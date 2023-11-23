using System.Text.Json;
using Microsoft.Extensions.Hosting;
using MyBlazorApp.Client.WebAssembly;
using OpenTelemetry.Exporter;

var builder = new WebAssemblyHostApplicationBuilder(args);

var httpClient = new HttpClient { BaseAddress = new(builder.HostEnvironment.BaseAddress) };
var wasmEnv = await httpClient.GetStringAsync("/wasm/wasm.env");
var jsonObj = JsonDocument.Parse(wasmEnv);

foreach (var element in jsonObj.RootElement.EnumerateObject())
{
    Environment.SetEnvironmentVariable(element.Name, element.Value.GetString()!);
}

builder.AddServiceDefaults();
builder.AddClientDefaults();
builder.ConfigureOpenTelemetry(new (ServiceName: "WebAssembly", Protocol: OtlpExportProtocol.HttpProtobuf));
builder.AddWasmEnvironmentVariables();

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