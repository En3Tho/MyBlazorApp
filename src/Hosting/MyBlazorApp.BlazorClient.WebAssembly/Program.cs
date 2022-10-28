using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyBlazorApp.BlazorClient.WebAssembly;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

await builder
    .ConfigureLogging()
    .ConfigureOptions()
    .AddServices()
    .Build()
    .RunAsync();