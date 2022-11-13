namespace rec En3Tho.FSharp.ComputationExpressions.BlazorBuilder.Core

open System.Runtime.CompilerServices
open En3Tho.FSharp.Extensions.GenericBuilderBase
open Microsoft.AspNetCore.Components
open Microsoft.AspNetCore.Components.Rendering

type BlazorBuilderCore(builder: RenderTreeBuilder) =
    let mutable sequenceCount = 0

    member _.Builder = builder

    member _.Advance() =
        sequenceCount <- sequenceCount + 1

    member this.AddContent(content: string) =
        builder.AddContent(sequenceCount, content)
        sequenceCount <- sequenceCount + 1

    member this.AddContent(content: RenderFragment) =
        builder.AddContent(sequenceCount, content)
        sequenceCount <- sequenceCount + 1

    member this.AddMarkupContent(content: string) =
        builder.AddMarkupContent(sequenceCount, content)
        sequenceCount <- sequenceCount + 1

    member _.OpenComponent<'a>() =
        builder.OpenComponent(sequenceCount, typeof<'a>)
        sequenceCount <- sequenceCount + 1

    member _.OpenElement(name: string) =
        builder.OpenElement(sequenceCount, name)
        sequenceCount <- sequenceCount + 1

    member _.AddAttribute(name: string) =
        builder.AddAttribute(sequenceCount, name)
        sequenceCount <- sequenceCount + 1

    member _.AddAttribute(name: string, value: obj) =
        builder.AddAttribute(sequenceCount, name, value)
        sequenceCount <- sequenceCount + 1

    member _.AddAttribute(name: string, value: bool) =
        builder.AddAttribute(sequenceCount, name, value)
        sequenceCount <- sequenceCount + 1

    member _.AddAttribute(name: string, value: string) =
        builder.AddAttribute(sequenceCount, name, value)
        sequenceCount <- sequenceCount + 1

    member _.AddAttribute(name: string, value: EventCallback) =
        builder.AddAttribute(sequenceCount, name, value)
        sequenceCount <- sequenceCount + 1

    member _.AddAttribute(name: string, value: EventCallback<'a>) =
        builder.AddAttribute(sequenceCount, name, value)
        sequenceCount <- sequenceCount + 1

    member _.CloseElement() =
        builder.CloseElement()

    member _.CloseComponent() =
        builder.CloseComponent()

type BlazorBuilderCode = BlazorBuilderCore -> unit
type BlazorBuilderAttributeCode = BlazorBuilderCore -> unit -> unit // erm

type RenderTreeBlockBase() =
    inherit UnitBuilderBase<BlazorBuilderCore>()

    // This is needed to preserve correct sequence counts in RenderTreeBuilder in case of non-yield operations (eg if ... then ...)
    // I'm not sure how does this work with while or for
    // maybe this isn't really needed? Need to check out diff mechanism
    member inline _.Zero() : BlazorBuilderCode =
        fun builder ->
            builder.Advance()

    member inline _.Yield([<InlineIfLambda>] codeBuilderCode: BlazorBuilderCode) : BlazorBuilderCode =
        fun builder ->
            codeBuilderCode builder

    member inline _.Yield([<InlineIfLambda>] codeBuilderCode: BlazorBuilderAttributeCode) : BlazorBuilderCode =
        fun builder ->
            codeBuilderCode builder ()

    member inline _.Yield(fragment: RenderFragment) : BlazorBuilderCode =
        fun builder ->
            builder.AddContent(fragment)

    member inline _.Yield(content: string) : BlazorBuilderCode =
        fun builder ->
            builder.AddContent(content: string)

    member inline _.Yield(markup: MarkupString) : BlazorBuilderCode =
        fun builder ->
            builder.AddMarkupContent(markup.Value)

    member inline _.Yield(data, value: 'a) : BlazorBuilderCode =
        fun builder ->
            builder.AddAttribute(data, value)

    member inline _.Yield(_: ComponentBlock<'a>) : BlazorBuilderCode =
        fun builder ->
            builder.OpenComponent<'a>()
            builder.CloseComponent()

    member inline _.Yield(value: ElementBlockBase<'name>) : BlazorBuilderCode =
        fun builder ->
            builder.OpenElement(value.Name)
            builder.CloseElement()

type IElementName = // TODO: static
    abstract Name: string

type ElementBlockBase<'name when 'name :> IElementName and 'name: struct>() =
    inherit RenderTreeBlockBase()

    member _.Name = Unchecked.defaultof<'name>.Name

    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderCode) : BlazorBuilderCode =
        fun builder ->
            builder.OpenElement(this.Name)
            runExpr builder
            builder.CloseElement()

type IAttribute =
    abstract RenderTo: builder: BlazorBuilderCore -> unit

type IAttributeName = // TODO: static
    abstract Name: string

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

[<Sealed>]
type AttributeBlock() =
    inherit UnitBuilderBase<BlazorBuilderCore>()

    member inline _.Yield<'attr when 'attr: struct and 'attr :> IAttribute>(attr: 'attr) : BlazorBuilderCode =
        fun builder ->
            attr.RenderTo builder

    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderCode) : BlazorBuilderAttributeCode =
        fun builder _ ->
            runExpr builder

[<Sealed>]
type ComponentBlock<'a when 'a :> ComponentBase>() =
    inherit UnitBuilderBase<BlazorBuilderCore>()

    member inline _.Yield<'attr when 'attr: struct and 'attr :> IAttribute>(attr: 'attr) : BlazorBuilderCode =
        fun builder ->
            attr.RenderTo builder

    static member val Instance = ComponentBlock<'a>()

    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderCode) : BlazorBuilderCode =
        fun builder ->
            builder.OpenComponent<'a>()
            runExpr builder
            builder.CloseComponent()

[<Sealed>]
type BlazorBuilderRunner() =

    inherit RenderTreeBlockBase()
    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderCode) =
        fun builder ->
            let builder = BlazorBuilderCore(builder)
            runExpr builder

[<Sealed>]
type RenderFragmentRunner() =

    inherit RenderTreeBlockBase()
    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderCode) : RenderFragment =
        RenderFragment(fun builder -> runExpr (BlazorBuilderCore(builder)))