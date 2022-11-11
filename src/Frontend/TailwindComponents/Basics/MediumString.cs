using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace TailwindComponents.Basics;

[Validatable<string>] // part to implement
internal readonly partial struct MediumString
{
    public static bool Validate([NotNullWhen(true)] string? value)
    {
        return value is { Length: < 10 };
    }

    public static string GetErrorMessage(string? value, [CallerArgumentExpression(nameof(value))] string argumentName = null!)
    {
        return $"Value must be valid string with length of 10: {argumentName}";
    }
}

// Source generated
internal readonly partial struct MediumString : IValidatable<MediumString, string>, IValidator<string>,
    IEquatable<MediumString>, IEquatable<string>
{
#if DEBUG
    private readonly Box<string> _value;
#else
    private readonly string _value;
#endif
    private MediumString(string value) => _value = value;
    public string Value => _value;

    public static bool Try(string? value, out MediumString result)
    {
        if (Validate(value))
        {
            result = new(value);
            return true;
        }

        result = default;
        return false;
    }

    public static MediumString Make(string value, [CallerArgumentExpression(nameof(value))] string argumentName = null!)
    {
        if (!Try(value, out var result))
        {
            throw new ArgumentException(GetErrorMessage(value, argumentName), nameof(value));
        }

        return result;
    }

    public override bool Equals(object? obj)
    {
        return obj is SmallString other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public bool Equals(MediumString other)
    {
        return Value == other.Value;
    }

    public bool Equals(string? other)
    {
        return Value == other;
    }

    public static implicit operator string(MediumString str) => str.Value;
}