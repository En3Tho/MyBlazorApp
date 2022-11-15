namespace En3Tho.FSharp.BlazorBuilder

open System
open En3Tho.FSharp.BlazorBuilder.Core
open En3Tho.FSharp.BlazorBuilder.Core.KnownAttributes
open Microsoft.AspNetCore.Components

[<AbstractClass; Sealed; AutoOpen>]
type StringAttributes() =
    static member private mk<'a when 'a :> IAttributeName and 'a: struct> value = Attribute<'a, string>(value)

    static member style' (value: string) = mk<Style>(value)
    static member class' (value: string) = mk<Class>(value)
    static member class' (value: string, value2: string) = mk<Class>(String.Concat(value, " ", value2))
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