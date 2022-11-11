using System.Diagnostics.CodeAnalysis;

namespace TailwindComponents.Basics;

internal interface IInputParser<T>
{
    static abstract bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider,
        [MaybeNullWhen(false)] out T result);
}

internal struct StringParser : IInputParser<string>
{
    public static bool TryParse(string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out string result)
    {
        if (s is { })
        {
            result = s;
            return true;
        }

        result = default;
        return false;
    }
}

internal struct ParsableParser<T> : IInputParser<T>
    where T : IParsable<T>
{
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider,
        [MaybeNullWhen(false)] out T result)
    {
        return T.TryParse(s, provider, out result);
    }
}