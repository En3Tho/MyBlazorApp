namespace TailwindComponents.Basics;

internal readonly struct Box<T> : IEquatable<Box<T>>
{
    private readonly T _value;

    public Box(T value)
    {
        _value = value;
    }

    public static implicit operator Box<T>(T value) => new(value);
    public static implicit operator T(Box<T> value) => value._value;

    public bool HasValue { get; } = true;
    public T Value => HasValue ? _value : throw new InvalidOperationException("Value not initialized");

    public override string ToString()
    {
        return HasValue ? _value?.ToString() ?? "" : "";
    }

    public bool Equals(Box<T> other)
    {
        return EqualityComparer<T>.Default.Equals(_value, other._value) && HasValue == other.HasValue;
    }

    public override bool Equals(object? obj)
    {
        return obj is Box<T> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_value, HasValue);
    }
}