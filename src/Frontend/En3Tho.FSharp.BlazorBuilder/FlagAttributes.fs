namespace En3Tho.FSharp.BlazorBuilder

open En3Tho.FSharp.BlazorBuilder.Core
open KnownAttributes

// TODO: are they really bool or just plain string?

[<AbstractClass; Sealed; AutoOpen>]
type FlagAttributes() =

    static member typeNumber' = Attribute<Type, _>("number") // TODO: more of these