using System.Runtime.CompilerServices;

namespace TailwindComponents.Basics;

//[Validatable<int>]
public record struct SmallNum(int Value) : IValidatable<SmallNum, int>, IValidator<int>
{
    public static bool Try(int value, out SmallNum result)
    {
        if (Validate(value))
        {
            result = new(value);
            return true;
        }

        result = default;
        return false;
    }

    public static SmallNum Make(int value, [CallerArgumentExpression(nameof(value))] string argumentName = null!)
    {
        if (!Try(value, out var result))
        {
            throw new ArgumentException(GetErrorMessage(value, argumentName), nameof(value));
        }

        return result;
    }

    public override string ToString() => Value.ToString();

    public static bool Validate(int value)
    {
        return value < 10;
    }

    public static string GetErrorMessage(int value, [CallerArgumentExpression(nameof(value))] string argumentName = null!)
    {
        return $"Value must be less than 10: {argumentName}";
    }
}