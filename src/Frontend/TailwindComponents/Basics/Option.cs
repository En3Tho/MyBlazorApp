namespace TailwindComponents.Basics;

public readonly struct Option<T>(T value) : IEquatable<Option<T>>
{
    readonly T _value = value;
    readonly bool _hasValue = true;

    public static implicit operator Option<T>(T value) => new(value);
    public static implicit operator T(Option<T> value) => value._value;

    public bool HasValue => _hasValue;
    public T Value => HasValue ? _value : throw new InvalidOperationException("Value not initialized");

    public override string ToString()
    {
        return Value?.ToString() ?? "";
    }

    public bool Equals(Option<T> other)
    {
        return EqualityComparer<T>.Default.Equals(Value, other.Value);
    }

    public override bool Equals(object? obj)
    {
        return obj is Option<T> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_value, HasValue);
    }

    public static bool operator ==(Option<T> left, Option<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Option<T> left, Option<T> right)
    {
        return !(left == right);
    }
}