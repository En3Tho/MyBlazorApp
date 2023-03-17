namespace TailwindComponents.Basics;

public readonly struct SafeBox<T> : IEquatable<SafeBox<T>>
{
    private readonly T _value;
    private readonly bool _hasValue;

    public SafeBox(T value)
    {
        _value = value;
        _hasValue = true;
    }

    public static implicit operator SafeBox<T>(T value) => new(value);
    public static implicit operator T(SafeBox<T> value) => value._value;

    public bool HasValue => _hasValue;
    public T Value => HasValue ? _value : throw new InvalidOperationException("Value not initialized");

    public override string ToString()
    {
        return Value?.ToString() ?? "";
    }

    public bool Equals(SafeBox<T> other)
    {
        return EqualityComparer<T>.Default.Equals(Value, other.Value);
    }

    public override bool Equals(object? obj)
    {
        return obj is SafeBox<T> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_value, HasValue);
    }

    public static bool operator ==(SafeBox<T> left, SafeBox<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SafeBox<T> left, SafeBox<T> right)
    {
        return !(left == right);
    }
}