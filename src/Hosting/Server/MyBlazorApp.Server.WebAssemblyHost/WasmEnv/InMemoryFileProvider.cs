using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

class WasmEnvFileProvider(IFileProvider webRootFileProvider, IFileInfo modifiedAppSettings) : IFileProvider
{
    public IDirectoryContents GetDirectoryContents(string subpath)
    {
        return webRootFileProvider.GetDirectoryContents(subpath);
    }

    public IFileInfo GetFileInfo(string subpath)
    {
        var fileInfo = webRootFileProvider.GetFileInfo(subpath);

        if (fileInfo is { Exists: true } && subpath.Equals($"/{WasmEnvFile.AppSettingsJson}", StringComparison.Ordinal))
        {
            return modifiedAppSettings;
        }

        return fileInfo;
    }

    public IChangeToken Watch(string filter)
    {
        return webRootFileProvider.Watch(filter);
    }
}