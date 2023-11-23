using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

class InMemoryFileProvider : IFileProvider
{
    private readonly InMemoryDirectoryContents _directoryContents;
    private readonly InMemoryFileInfo _fileInfo;

    public InMemoryFileProvider()
    {
        _fileInfo = WasmEnvFile.CreateFromEnvironment();
        _directoryContents = new InMemoryDirectoryContents([_fileInfo]);
    }

    public IDirectoryContents GetDirectoryContents(string subpath)
    {
        if (subpath.Equals("/"))
        {
            return _directoryContents;
        }

        return NotFoundDirectoryContents.Singleton;
    }

    public IFileInfo GetFileInfo(string subpath)
    {
        if (subpath.Equals($"/{WasmEnvFile.WasmEnv}"))
        {
            return _fileInfo;
        }

        return new NotFoundFileInfo(subpath);
    }

    public IChangeToken Watch(string filter)
    {
        return NullChangeToken.Singleton;
    }
}