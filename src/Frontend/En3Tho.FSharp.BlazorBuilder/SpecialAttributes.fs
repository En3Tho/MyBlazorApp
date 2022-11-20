namespace En3Tho.FSharp.BlazorBuilder

open System
open System.Runtime.CompilerServices
open En3Tho.FSharp.BlazorBuilder.Core
open Microsoft.AspNetCore.Components

#nowarn "42"

type [<Struct; IsReadOnly>] RefAttribute(action: Action<obj>) =
    interface IAttribute with
        member this.RenderTo(builder) =
            builder.AddComponentReferenceCapture(action)

[<AbstractClass; Sealed; AutoOpen>]
type SpecialAttributes() =
    static member class' (value: string) =
        Attribute<KnownAttributes.Class, _>(value)

    static member class' (value: string, value2: string) =
        if String.IsNullOrEmpty value then SpecialAttributes.class' value2
        elif String.IsNullOrEmpty value2 then SpecialAttributes.class' value
        else Attribute<KnownAttributes.Class, _>(String.Concat(value, " ", value2))

    // TODO: test these
    // What if string is empty? We do not need to allocate that
    // Reuse logic from Concat here I guess
    static member class' (value: string, value2: string, value3: string) =
        if String.IsNullOrEmpty value then SpecialAttributes.class'(value2, value3)
        elif String.IsNullOrEmpty value2 then SpecialAttributes.class'(value, value3)
        elif String.IsNullOrEmpty value3 then SpecialAttributes.class'(value, value2)
        else
            // TODO: unsafe span via C# and magic extensions in F# :D
            let str = String(char 0, 2 + value.Length + value2.Length + value3.Length)
            let mutable span = (# "" (str.AsSpan()): Span<char> #)
            // TODO: unsafe span
            value.CopyTo(span)
            span <- span.Slice(value.Length)
            span[0] <- ' '
            span <- span.Slice(1)
            value2.CopyTo(span)
            span <- span.Slice(value2.Length)
            span[0] <- ' '
            span <- span.Slice(1)
            value3.CopyTo(span)
            Attribute<KnownAttributes.Class, _>(str)

    static member class' (value: string, value2: string, value3: string, value4: string) =
        if String.IsNullOrEmpty value then SpecialAttributes.class'(value2, value3, value4)
        elif String.IsNullOrEmpty value2 then SpecialAttributes.class'(value, value3, value4)
        elif String.IsNullOrEmpty value3 then SpecialAttributes.class'(value, value2, value4)
        elif String.IsNullOrEmpty value4 then SpecialAttributes.class'(value, value2, value3)
        else
            let str = String(char 0, 3 + value.Length + value2.Length + value3.Length + value4.Length)
            let mutable span = (# "" (str.AsSpan()): Span<char> #)

            value.CopyTo(span)
            span <- span.Slice(value.Length)
            span[0] <- ' '
            span <- span.Slice(1)
            value2.CopyTo(span)
            span <- span.Slice(value2.Length)
            span[0] <- ' '
            span <- span.Slice(1)
            value3.CopyTo(span)
            span <- span.Slice(value3.Length)
            span[0] <- ' '
            span <- span.Slice(1)
            value4.CopyTo(span)
            Attribute<KnownAttributes.Class, _>(str)

    static member ChildContent' (value: RenderFragment) = Attribute<KnownAttributes.ChildContent, _>(value)
    static member ChildContent' (value: RenderFragment<'a>) = Attribute<KnownAttributes.ChildContent, _>(value)
    static member inline ref' ([<InlineIfLambda>] action: 'T -> unit) = RefAttribute(fun obj -> action (obj :?> 'T))
    static member ref' (action: Action<obj>) = RefAttribute(action)