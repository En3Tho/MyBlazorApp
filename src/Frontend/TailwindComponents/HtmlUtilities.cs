using Microsoft.AspNetCore.Components;

namespace TailwindComponents;

public static class HtmlUtilities
{
    public static string If(bool condition, string style) => condition ? style : "";
    public static string If(bool condition, string style, string other) => condition ? style : other;
    public static RenderFragment If(bool condition, RenderFragment style) => condition ? style : _ => { };
    public static RenderFragment If(bool condition, RenderFragment style, RenderFragment other) => condition ? style : other;
    public static RenderFragment<T> If<T>(bool condition, RenderFragment<T> style) => condition ? style : _ => _ => { };
    public static RenderFragment<T> If<T>(bool condition, RenderFragment<T> style, RenderFragment<T> other) => condition ? style : other;
}