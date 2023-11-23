using System.Text.Json;
using Microsoft.Extensions.Hosting;

namespace MyBlazorApp.Client.WebAssembly;

public static class Extensions
{
    public static IHostApplicationBuilder AddWasmEnvironmentVariables(this IHostApplicationBuilder builder)
    {
        // var env = File.ReadAllText("env.json");
        // var jsonObj = JsonDocument.Parse(env);
        //
        // foreach (var element in jsonObj.RootElement.EnumerateObject())
        // {
        //     Environment.SetEnvironmentVariable(element.Name, element.Value.GetString()!);
        // }

        builder.Configuration.AddEnvironmentVariables();
        return builder;
    }
}