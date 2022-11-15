namespace En3Tho.FSharp.BlazorBuilder

open System
open En3Tho.FSharp.BlazorBuilder.Core
open En3Tho.FSharp.BlazorBuilder.Core.KnownAttributes
open Microsoft.AspNetCore.Components

#nowarn "42"

[<AbstractClass; Sealed; AutoOpen>]
type StringAttributes() =
    static member private mk<'a when 'a :> IAttributeName and 'a: struct> value = Attribute<'a, string>(value)

    static member style' (value: string) = mk<Style>(value)

    // TODO: find a way not to allocate same strings?
    // Literals in F# do not get concatenated automatically so this can lead to unneeded allocations
    // Razor usually tries to optimize this when possible
    // Apply intern optimization for class' or make an internal true structure to avoid allocations of the same strings?
    static member class' (value: string) = mk<Class>(value)
    static member class' (value: string, value2: string) =
        mk<Class>(String.Concat(value, " ", value2))

    // TODO: test these
    static member class' (value: string, value2: string, value3: string) =
        let str = String(char 0, 2 + value.Length + value2.Length + value3.Length)
        let span = (# "" (str.AsSpan()): Span<char> #)
        value.CopyTo(span)
        span[value.Length] <- ' '
        value2.CopyTo(span.Slice(value.Length + 1))
        span[value.Length + 1 + value2.Length] <- ' '
        value3.CopyTo(span.Slice(value.Length + 1 + value2.Length + 1))
        mk<Class>(str)

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
        mk<Class>(str)

    // TODO: class' 3-4-5 overloads
    static member type' (value: string) = mk<Type>(value)
    static member id' (value: string) = attr("id", value)
    static member href' (value: string) = attr("href", value)
    static member src' (value: string) = attr("src", value)
    static member alt' (value: string) = attr("alt", value)
    static member title' (value: string) = attr("title", value)
    static member value' (value: string) = mk<Value>(value)
    static member placeholder' (value: string) = attr("placeholder", value)
    static member name' (value: string) = attr("name", value)
    static member for' (value: string) = attr("for", value)
    static member ChildContent' (value: RenderFragment) = Attribute<ChildContent, _>(value)
    static member ChildContent' (value: RenderFragment<'a>) = Attribute<ChildContent, _>(value)