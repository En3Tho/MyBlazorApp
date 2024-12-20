using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components;

namespace TailwindComponents;

public static class HtmlUtilities
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string If(bool condition, string style) => condition ? style : "";
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string If(bool condition, string style, string other) => condition ? style : other;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RenderFragment If(bool condition, RenderFragment style) => condition ? style : _ => { };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RenderFragment If(bool condition, RenderFragment style, RenderFragment other) => condition ? style : other;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RenderFragment<T> If<T>(bool condition, RenderFragment<T> style) => condition ? style : _ => _ => { };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RenderFragment<T> If<T>(bool condition, RenderFragment<T> style, RenderFragment<T> other) => condition ? style : other;
}