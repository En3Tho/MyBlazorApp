using System.Text.Json.Nodes;
using Microsoft.Extensions.Hosting;

namespace MyBlazorApp.Client.WebAssembly;

public static class Extensions
{
    public static IHostApplicationBuilder AddWasmEnvironmentVariables(this IHostApplicationBuilder builder)
    {
        var appsettings = File.ReadAllText("appsettings.json");
        var jsonObj = JsonNode.Parse(appsettings)!;

        if (jsonObj["EnvironmentVariables"] is {} environmentNode)
        {
            foreach (var element in environmentNode.AsObject())
            {
                Environment.SetEnvironmentVariable(element.Key, element.Value!.ToString());
            }
        }

        return builder;
    }
}