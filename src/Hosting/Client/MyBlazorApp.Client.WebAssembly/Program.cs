using System.Text.Json;
using Microsoft.Extensions.Hosting;

var builder = new WebAssemblyHostApplicationBuilder(args);

var httpClient = new HttpClient { BaseAddress = new(builder.Builder.HostEnvironment.BaseAddress) };
var wasmEnv = await httpClient.GetStringAsync("/wasm/wasm.env");
var jsonObj = JsonDocument.Parse(wasmEnv);

foreach (var element in jsonObj.RootElement.EnumerateObject())
{
    Environment.SetEnvironmentVariable(element.Name, element.Value.GetString()!);
}

builder.AddServiceDefaults();
builder.AddClientDefaults();
builder.ConfigureClientOpenTelemetry(new (ServiceName: "WebAssembly"));
builder.Configuration.AddEnvironmentVariables();

var host = builder.Build();
var hostedServices = host.Services.GetRequiredService<IEnumerable<IHostedService>>().ToArray();

foreach (var hostedService in hostedServices)
{
    // can block app start? 1 thread in wasm
    await hostedService.StartAsync(CancellationToken.None);
}

try
{
    await host.RunAsync();
}
finally
{
    // TODO: this won't be ever called will it?
    // check and delete the code if needed
    foreach (var hostedService in hostedServices)
    {
        await hostedService.StopAsync(CancellationToken.None);
    }
}