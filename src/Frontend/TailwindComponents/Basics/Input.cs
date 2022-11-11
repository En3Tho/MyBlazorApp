namespace TailwindComponents.Basics;

internal sealed class StringInput<TValidatable> : Input<TValidatable, string, StringParser>
    where TValidatable : IValidatable<TValidatable, string>
{
    public StringInput() : base()
    {
    }

    public StringInput(TValidatable value) : base(value)
    {
    }
}

internal sealed class IntInput<TValidatable> : Input<TValidatable, int, ParsableParser<int>>
    where TValidatable : IValidatable<TValidatable, int>
{
    public IntInput() : base()
    {
    }

    public IntInput(TValidatable value) : base(value)
    {
    }
}
