using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyBlazorApp.ComponentsAndPages.Shared;
using MyBlazorApp.Client.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMyBlazorAppClient(builder.Configuration);
builder.Logging.ConfigureLogging(builder.Configuration);
builder.Configuration.ConfigureOptions();

await builder.Build().RunAsync();