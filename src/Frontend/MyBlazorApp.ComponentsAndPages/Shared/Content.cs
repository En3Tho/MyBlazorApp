namespace MyBlazorApp.ComponentsAndPages.Shared;

internal static class Content
{
    public static string MakePath(string pathToContentFromWwwRoot) =>
        $"_content/MyBlazorApp.ComponentsAndPages/{pathToContentFromWwwRoot}";
}