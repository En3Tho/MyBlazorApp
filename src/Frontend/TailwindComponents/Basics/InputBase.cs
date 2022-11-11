namespace TailwindComponents.Basics;

internal abstract class StrictInput<TValidatable, TData, TParser> : InputBase<TValidatable, TData, TParser, InputMode.Strict>
    where TValidatable : IValidatable<TValidatable, TData> where TParser : struct, IInputParser<TData>
{
    protected StrictInput() : base()
    {
    }

    protected StrictInput(TValidatable value) : base(value)
    {
    }
}

internal abstract class Input<TValidatable, TData, TParser> : InputBase<TValidatable, TData, TParser, InputMode.Loose>
    where TValidatable : IValidatable<TValidatable, TData> where TParser : struct, IInputParser<TData>
{
    protected Input() : base()
    {
    }

    protected Input(TValidatable value) : base(value)
    {
    }
}

internal interface IInputMode
{
}

internal static class InputMode
{
    internal struct Strict : IInputMode
    {
    }

    internal struct Loose : IInputMode
    {
    }
}

internal abstract class InputBase<TValidatable, TData, TParser, TInputMode>
    where TValidatable : IValidatable<TValidatable, TData>
    where TParser : struct, IInputParser<TData>
    where TInputMode : struct, IInputMode
{
    private string? _rawString = "";
    public Box<TValidatable> Value { get; private set; }

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

    public void SetValue(TValidatable value)
    {
        Value = value;
        _rawString = Value.ToString();
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

    public string? Raw
    {
        get => _rawString;
        set
        {
            if (typeof(TInputMode) == typeof(InputMode.Strict))
            {
                if (string.IsNullOrEmpty(value))
                {
                    Value = default;
                    _rawString = "";
                }
                else
                {
                    if (TParser.TryParse(value, null, out var parsed)
                        && TValidatable.Try(parsed, out var result))
                    {
                        Value = result;
                        _rawString = value;
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(value))
                {
                    Value = default;
                }
                else
                {
                    // in non strict mode we need to save raw string but clear value if it is invalid
                    // to make sure is valid check works correctly
                    if (TParser.TryParse(value, null, out var parsed)
                        && TValidatable.Try(parsed, out var result))
                    {
                        Value = result;
                    }
                    else
                    {
                        Value = default;
                    }
                }

                _rawString = value;
            }
        }
    }
}