using System.Runtime.CompilerServices;

namespace TailwindComponents.Basics;

internal readonly struct SmallString : IValidatable<SmallString, string>, IEquatable<SmallString>
{
#if DEBUG
    // use box in debug to generate an exception if value was not set correctly
    // TODO: maybe CHECKED is a better flag for this?
    private readonly Box<string> _value;
#else
    // in release mode use a simple string
    private readonly string _value;
#endif
    private SmallString(string value) => _value = value;
    public string Value => _value;

    public override string ToString() => Value.ToString();

    public static bool Try(string? value, out SmallString result)
    {
        if (value is { Length: < 3 })
        {
            result = new(value);
            return true;
        }

        result = default;
        return false;
    }

    public static SmallString Make(string value, [CallerArgumentExpression(nameof(value))] string argumentName = null!)
    {
        if (!Try(value, out var result))
        {
            throw new ArgumentException("Value must be less than 3 characters", nameof(value));
        }

        return result;
    }

    public bool Equals(SmallString other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is SmallString other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}