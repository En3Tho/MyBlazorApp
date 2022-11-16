namespace En3Tho.FSharp.BlazorBuilder

open En3Tho.FSharp.BlazorBuilder.Core
open En3Tho.FSharp.BlazorBuilder.Core.KnownAttributes

[<AbstractClass; Sealed; AutoOpen>]
type BoolAttributes() =
    static member checked' = attr("checked")
    static member disabled' = attr("disabled")
    static member autofocus' = attr("autofocus")
    static member required' = attr("required")
    static member multiple' = attr("multiple")
    static member selected' = attr("selected")
    static member readonly' = attr("readonly")
    static member novalidate' = attr("novalidate")
    static member hidden' = attr("hidden")
    static member typeNumber' = Attribute<Type, _>("number") // TODO: more of these