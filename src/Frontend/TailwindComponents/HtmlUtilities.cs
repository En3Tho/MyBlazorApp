namespace TailwindComponents;

public static class HtmlUtilities
{
    public static string If(bool condition, string style) => condition ? style : "";
    public static string If(bool condition, string style, string other) => condition ? style : other;
    public const string hover = "hover:";
    public const string focus = "focus:";
}