namespace En3Tho.FSharp.ComputationExpressions.BlazorBuilder

// TODO: are they really bool or just plain string?

[<AbstractClass; Sealed; AutoOpen>]
type BoolAttributes() =
    static member checked' = attr("checked", true)
    static member disabled' = attr("disabled", true)
    static member autofocus' = attr("autofocus", true)
    static member required' = attr("required", true)
    static member multiple' = attr("multiple", true)
    static member selected' = attr("selected", true)
    static member readonly' = attr("readonly", true)
    static member novalidate' = attr("novalidate", true)
    static member hidden' = attr("hidden", true)