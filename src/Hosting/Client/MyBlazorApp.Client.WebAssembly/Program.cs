using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting;

var builder = new WebAssemblyHostApplicationBuilder(args);

builder.AddMyBlazorAppClient();
builder.AddClientDefaults();
builder.ConfigureClientOpenTelemetry(new (ServiceName: "WebAssembly"));

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