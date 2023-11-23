using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.StaticFiles;

class InMemoryContentTypeProvider : IContentTypeProvider
{
    public bool TryGetContentType(string subpath, [NotNullWhen(true)] out string? contentType)
    {
        if (subpath.Equals($"/{WasmEnvFile.WasmEnv}"))
        {
            contentType = "application/json";
            return true;
        }

        contentType = null;
        return false;
    }
}