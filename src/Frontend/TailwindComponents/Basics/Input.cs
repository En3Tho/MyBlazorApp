namespace TailwindComponents.Basics;

public sealed class StringInput<TValidatable> : Input<TValidatable, string, StringParser>
    where TValidatable : IValidatable<TValidatable, string>
{
    public StringInput() {
    }

    public StringInput(TValidatable value) : base(value)
    {
    }
}

public sealed class IntInput<TValidatable> : Input<TValidatable, int, ParsableParser<int>>
    where TValidatable : IValidatable<TValidatable, int>
{
    public IntInput() {
    }

    public IntInput(TValidatable value) : base(value)
    {
    }
}
