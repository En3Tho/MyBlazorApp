using System.Collections;
using Microsoft.Extensions.FileProviders;

class InMemoryDirectoryContents(IEnumerable<InMemoryFileInfo> contents) : IDirectoryContents
{
    public IEnumerator<IFileInfo> GetEnumerator() => contents.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => contents.GetEnumerator();

    public bool Exists => true;
}