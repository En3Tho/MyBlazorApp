namespace En3Tho.FSharp.BlazorBuilder.Core

open System
open System.Runtime.CompilerServices
open Microsoft.AspNetCore.Components
open Microsoft.AspNetCore.Components.Rendering

type BlazorBuilderCore(builder: RenderTreeBuilder) =
    let mutable sequenceCount = 0

    member _.Builder with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        builder

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.GetSequenceCountForWhileExpression() =
        sequenceCount

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.ResumeSequenceAtStartOfWhileExpression(value) =
        sequenceCount <- value

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.Advance() =
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.AddContent(content: string) =
        builder.AddContent(sequenceCount, content)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.AddContent(content: RenderFragment) =
        builder.AddContent(sequenceCount, content)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.AddMarkupContent(content: string) =
        builder.AddMarkupContent(sequenceCount, content)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.OpenComponent<'a>() =
        builder.OpenComponent(sequenceCount, typeof<'a>)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.OpenElement(name: string) =
        builder.OpenElement(sequenceCount, name)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.AddAttribute(name: string) =
        builder.AddAttribute(sequenceCount, name)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.AddAttribute(name: string, value: obj) =
        builder.AddAttribute(sequenceCount, name, value)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.AddAttribute(name: string, value: bool) =
        builder.AddAttribute(sequenceCount, name, value)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.AddAttribute(name: string, value: string) =
        builder.AddAttribute(sequenceCount, name, value)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.AddAttribute(name: string, value: MulticastDelegate) =
        builder.AddAttribute(sequenceCount, name, value)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.AddAttribute(name: string, value: EventCallback) =
        builder.AddAttribute(sequenceCount, name, value)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.AddAttribute(name: string, value: EventCallback<'a>) =
        builder.AddAttribute(sequenceCount, name, value)
        sequenceCount <- sequenceCount + 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.CloseElement() =
        builder.CloseElement()

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member _.CloseComponent() =
        builder.CloseComponent()

type IAttribute =
    abstract RenderTo: builder: BlazorBuilderCore -> unit

type IElementName = // TODO: static
    abstract Name: string

type IAttributeName = // TODO: static
    abstract Name: string

type IComponentImport =
    abstract Builder: BlazorBuilderCore

type BlazorBuilderCode = delegate of BlazorBuilderCore -> unit
type BlazorBuilderComponentCode = BlazorBuilderCore -> unit -> unit
type ComponentImportCode<'a when 'a: struct and 'a :> IComponentImport> = BlazorBuilderCore -> 'a

type [<Struct; IsReadOnly>] Attribute<'name when 'name: struct and 'name :> IAttributeName> =
    member _.Name = Unchecked.defaultof<'name>.Name

    interface IAttribute with
        member this.RenderTo(builder) = builder.AddAttribute(this.Name)

type [<Struct; IsReadOnly>] Attribute<'name, 'value when 'name: struct and 'name :> IAttributeName>(value: 'value) =
    member _.Name = Unchecked.defaultof<'name>.Name
    member _.Value = value

    interface IAttribute with
        member this.RenderTo(builder) = builder.AddAttribute(this.Name, this.Value)

type [<Struct; IsReadOnly>] CustomAttribute(name: string) =
    member _.Name = name

    interface IAttribute with
        member this.RenderTo(builder) = builder.AddAttribute(this.Name)

type [<Struct; IsReadOnly>] CustomAttribute<'a>(name: string, value: 'a) =
    member _.Name = name
    member _.Value = value

    interface IAttribute with
        member this.RenderTo(builder) = builder.AddAttribute(this.Name, this.Value)