using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace TailwindComponents.Basics;

internal interface IValidator<T>
{
    static abstract bool Validate([NotNullWhen(true)] T? value);

    static abstract string GetErrorMessage(T? value,
        [CallerArgumentExpression(nameof(value))] string argumentName = null!);
}

internal interface IValidatable<TSelf, TData>
{
    static abstract bool Try([NotNullWhen(true)] TData? value, [MaybeNullWhen(false)] out TSelf result);
    static abstract TSelf Make(TData value, [CallerArgumentExpression(nameof(value))] string argumentName = null!);
    TData Value { get; }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class Validatable<T> : Attribute
{
}