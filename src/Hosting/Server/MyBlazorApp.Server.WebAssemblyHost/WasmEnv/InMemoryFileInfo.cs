using Microsoft.Extensions.FileProviders;

class InMemoryFileInfo(string name, byte[] file, DateTimeOffset lastModified) : IFileInfo
{
    public Stream CreateReadStream()
    {
        return new MemoryStream(file, writable: false);
    }

    public bool Exists => true;
    public bool IsDirectory => false;
    public DateTimeOffset LastModified => lastModified;
    public long Length => file.Length;
    public string Name => name;
    public string? PhysicalPath => null;
}