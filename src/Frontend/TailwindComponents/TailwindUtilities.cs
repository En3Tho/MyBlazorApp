namespace TailwindComponents;

public static class HtmlUtilities
{
    public static string If(bool condition, string style) => condition ? style : "";
    public static string If(bool condition, string style, string other) => condition ? style : other;
}

// TODO: remove this if it doesn't really help (so far it didn't)
public class Ref<T>
{
    public Ref(T value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value!.ToString()!;
    }

    public override int GetHashCode()
    {
        return Value!.GetHashCode();
    }

    public T Value { get; set; }

    public static implicit operator T (Ref<T> @ref) => @ref.Value;
    public static implicit operator Ref<T> (T value) => new(value);
}