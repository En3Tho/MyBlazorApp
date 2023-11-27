using System.Collections;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.FileProviders;

static class WasmEnvFile
{
    public const string WasmEnvPrefix = "WASM__";
    public const string AppSettingsJson = "appsettings.json";

    public static InMemoryFileInfo CreateFromEnvironment(JsonNode? appSettings)
    {
        appSettings ??= new JsonObject();
        var envNode = new JsonObject();

        appSettings["EnvironmentVariables"] = envNode;

        foreach (DictionaryEntry env in (Hashtable)Environment.GetEnvironmentVariables())
        {
            if (env.Key is string key && key.StartsWith(WasmEnvPrefix))
            {
                envNode[key[WasmEnvPrefix.Length..]] = env.Value!.ToString();
            }
        }

        var bytes = JsonSerializer.SerializeToUtf8Bytes(appSettings);
        return new(AppSettingsJson, bytes, DateTimeOffset.Now);
    }

    public static async Task<IFileInfo> CreateAppSettingsJson(IFileInfo appSettingsInfo)
    {
        if (appSettingsInfo is { Exists: true })
        {
            var obj = await JsonNode.ParseAsync(appSettingsInfo.CreateReadStream());
            return CreateFromEnvironment(obj);
        }

        return CreateFromEnvironment(null);
    }
}