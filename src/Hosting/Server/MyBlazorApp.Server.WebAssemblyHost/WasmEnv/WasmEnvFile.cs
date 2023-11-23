using System.Collections;
using System.Text.Json;
using System.Text.Json.Nodes;

static class WasmEnvFile
{
    public const string WasmEnvPrefix = "WASM__";
    public const string WasmEnv = "env.json";

    public static InMemoryFileInfo CreateFromEnvironment()
    {
        var jsonObject = new JsonObject();

        foreach (DictionaryEntry env in (Hashtable)Environment.GetEnvironmentVariables())
        {
            if (env.Key is string key && key.StartsWith(WasmEnvPrefix))
            {
                jsonObject[key[WasmEnvPrefix.Length..]] = env.Value!.ToString();
            }
        }

        var bytes = JsonSerializer.SerializeToUtf8Bytes(jsonObject);
        return new(WasmEnv, bytes, DateTimeOffset.Now);
    }
}