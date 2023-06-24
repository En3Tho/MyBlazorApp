using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBlazorApp.ComponentsAndPages.Shared;
using MyBlazorApp.Client.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMyBlazorAppClient(builder.Configuration);
builder.Logging.ConfigureLogging(builder.Configuration);
builder.Configuration.ConfigureOptions();

// TODO: it's better to build services locally here?

var host = builder.Build();
var hostedServices = host.Services.GetRequiredService<IEnumerable<IHostedService>>().ToArray();

foreach (var hostedService in hostedServices)
{
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
