namespace En3Tho.FSharp.BlazorBuilder

open System
open En3Tho.FSharp.BlazorBuilder.Core
open Microsoft.AspNetCore.Components

#nowarn "42"

[<AbstractClass; Sealed; AutoOpen>]
type SpecialAttributes() =
    static member class' (value: string) =
        Attribute<KnownAttributes.Class, _>(value)

    static member class' (value: string, value2: string) =
        Attribute<KnownAttributes.Class, _>(String.Concat(value, " ", value2))

    // TODO: test these
    // What if string is empty? We do not need to allocate that
    // Reuse logic from Concat here I guess
    static member class' (value: string, value2: string, value3: string) =
        let str = String(char 0, 2 + value.Length + value2.Length + value3.Length)
        let span = (# "" (str.AsSpan()): Span<char> #)
        value.CopyTo(span)
        span[value.Length] <- ' '
        value2.CopyTo(span.Slice(value.Length + 1))
        span[value.Length + 1 + value2.Length] <- ' '
        value3.CopyTo(span.Slice(value.Length + 1 + value2.Length + 1))
        Attribute<KnownAttributes.Class, _>(str)

    static member class' (value: string, value2: string, value3: string, value4: string) =
        let str = String(char 0, 3 + value.Length + value2.Length + value3.Length + value4.Length)
        let span = (# "" (str.AsSpan()): Span<char> #)
        value.CopyTo(span)
        span[value.Length] <- ' '
        value2.CopyTo(span.Slice(value.Length + 1))
        span[value.Length + 1 + value2.Length] <- ' '
        value3.CopyTo(span.Slice(value.Length + 1 + value2.Length + 1))
        span[value.Length + 1 + value2.Length + 1 + value3.Length] <- ' '
        value4.CopyTo(span.Slice(value.Length + 1 + value2.Length + 1 + value3.Length + 1))
        Attribute<KnownAttributes.Class, _>(str)

    static member ChildContent' (value: RenderFragment) = Attribute<KnownAttributes.ChildContent, _>(value)
    static member ChildContent' (value: RenderFragment<'a>) = Attribute<KnownAttributes.ChildContent, _>(value)