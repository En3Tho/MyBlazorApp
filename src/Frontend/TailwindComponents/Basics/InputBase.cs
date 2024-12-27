namespace TailwindComponents.Basics;

public interface IInput<T>
{
    T Value { get; }
    bool IsEmpty { get; }
    bool IsValid { get; }
    string? Raw { get; set; }
    public Action<T>? OnValueChange { get; init; }
    public Action<string?>? OnRawChange { get; init; }
}

public abstract class StrictInput<TValidatable, TData, TParser> : InputBase<TValidatable, TData, TParser, InputMode.Strict>
    where TValidatable : IValidatable<TValidatable, TData> where TParser : struct, IInputParser<TData>
{
    protected StrictInput() {
    }

    protected StrictInput(TValidatable value) : base(value)
    {
    }
}

public abstract class Input<TValidatable, TData, TParser> : InputBase<TValidatable, TData, TParser, InputMode.Loose>
    where TValidatable : IValidatable<TValidatable, TData> where TParser : struct, IInputParser<TData>
{
    protected Input() {
    }

    protected Input(TValidatable value) : base(value)
    {
    }
}

public interface IInputMode
{
}

public static class InputMode
{
    public struct Strict : IInputMode
    {
    }

    public struct Loose : IInputMode
    {
    }
}

public abstract class InputBase<TValidatable, TData, TParser, TInputMode> : IInput<Option<TValidatable>>
    where TValidatable : IValidatable<TValidatable, TData>
    where TParser : struct, IInputParser<TData>
    where TInputMode : struct, IInputMode
{
    private string? _rawString = "";
    public Option<TValidatable> Value { get; private set; }
    public Action<Option<TValidatable>>? OnValueChange { get; init; }
    public Action<string?>? OnRawChange { get; init; }

    protected InputBase()
    {
    }

    protected InputBase(TValidatable value) : this()
    {
        SetValue(value);
    }

    public override string ToString()
    {
        return _rawString ?? "";
    }

    private void SetValue(Option<TValidatable> value, string? raw = null)
    {
        var rawString = raw ?? value.ToString();
        Value = value;
        _rawString = rawString;
        OnRawChange?.Invoke(rawString);
        OnValueChange?.Invoke(value);
    }

    private void Clear()
    {
        Value = default;
        _rawString = "";
        OnRawChange?.Invoke("");
        OnValueChange?.Invoke(default);
    }

    public string? Raw
    {
        get => _rawString;
        set
        {
            if (typeof(TInputMode) == typeof(InputMode.Strict))
            {
                if (string.IsNullOrEmpty(value))
                {
                    Clear();
                }
                else
                {
                    if (TParser.TryParse(value, null, out var parsed)
                        && TValidatable.Try(parsed, out var result))
                    {
                        SetValue(result, value);
                    }
                }
            }
            else
            {
                // in non strict mode we need to save raw string but clear value if it is invalid
                // to make sure is valid check works correctly
                var result =
                    string.IsNullOrEmpty(value)
                    && TParser.TryParse(value, null, out var parsed)
                    && TValidatable.Try(parsed, out var validated)
                        ? new Option<TValidatable>(validated)
                        : default;
                SetValue(result, value);
            }
        }
    }

    public bool IsEmpty => string.IsNullOrEmpty(_rawString);

    public bool IsValid
    {
        get
        {
            if (typeof(TInputMode) == typeof(InputMode.Strict))
            {
                return Value.HasValue;
            }

            return IsEmpty || Value.HasValue;
        }
    }
}