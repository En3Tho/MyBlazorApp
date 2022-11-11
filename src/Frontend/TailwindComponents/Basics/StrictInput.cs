using System.Diagnostics.CodeAnalysis;

namespace TailwindComponents.Basics;

public sealed class StrictStringInput<TValidatable> : StrictInput<TValidatable, string, StringParser>
    where TValidatable : IValidatable<TValidatable, string>
{
    public StrictStringInput() : base()
    {
    }

    public StrictStringInput(TValidatable value) : base(value)
    {
    }
}

public sealed class StrictIntInput<TValidatable> : StrictInput<TValidatable, int, ParsableParser<int>>
    where TValidatable : IValidatable<TValidatable, int>
{
    public StrictIntInput() : base()
    {
    }

    public StrictIntInput(TValidatable value) : base(value)
    {
    }
}

