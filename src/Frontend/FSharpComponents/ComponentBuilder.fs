namespace En3Tho.FSharp.ComputationExpressions.ComponentBuilder

open System.Runtime.CompilerServices
open En3Tho.FSharp.Extensions.GenericBuilderBase
open Microsoft.AspNetCore.Components
open Microsoft.AspNetCore.Components.Rendering
open Microsoft.FSharp.Core

module rec ComponentBuilderImpl =

    type ComponentBuilder(builder: RenderTreeBuilder) =
        let mutable sequenceCount = 0

        member this.AddMarkup(content: string) =
            builder.AddMarkupContent(sequenceCount, content)
            sequenceCount <- sequenceCount + 1

        member _.OpenComponent<'a>() =
            builder.OpenComponent(sequenceCount, typeof<'a>)
            sequenceCount <- sequenceCount + 1 // when to close?

        member _.OpenElement(name: string) =
            builder.OpenElement(sequenceCount, name)
            sequenceCount <- sequenceCount + 1 // when to close?

        member _.AddAttribute(name: string, value: obj) =
            builder.AddAttribute(sequenceCount, name, value)
            sequenceCount <- sequenceCount + 1

        member _.CloseElement() =
            builder.CloseElement()

        member _.CloseComponent() =
            builder.CloseComponent()

    type [<Struct; IsReadOnly>] Attribute<'a>(name: string, value: 'a) =
        member _.Name = name
        member _.Value = value

    type [<Struct; IsReadOnly>] AttributeName<'a>(name: string) =
        member _.Name = name
        static member (=>) (attr: AttributeName<'a>, value: 'a) = Attribute<'a>(attr.Name, value)

    type ComponentBuilderCode = ComponentBuilder -> unit

    type ComponentBlockBase() =
        inherit UnitBuilderBase<ComponentBuilder>()

        member inline _.Yield([<InlineIfLambda>] codeBuilderCode: ComponentBuilderCode) : ComponentBuilderCode =
            fun builder ->
                codeBuilderCode builder

        member inline _.Yield(markup) : ComponentBuilderCode =
            fun builder ->
                builder.AddMarkup(markup)

        member inline _.Yield(data, value: 'a) : ComponentBuilderCode =
            fun builder ->
                builder.AddAttribute(data, value)

        member inline _.Yield(_: ComponentBlock<'a>) : ComponentBuilderCode =
            fun builder ->
                builder.OpenComponent<'a>()
                builder.CloseComponent()

        member inline _.Yield(attr: Attribute<'a>) : ComponentBuilderCode =
            fun builder ->
                builder.AddAttribute(attr.Name, attr.Value)

        member inline _.Yield(value: ElementBlockBase) : ComponentBuilderCode =
            fun builder ->
                builder.OpenElement(value.Name)
                builder.CloseElement()

    [<Sealed>]
    type ComponentBuilderRunner(builder: RenderTreeBuilder) =

        inherit ComponentBlockBase()
        member inline this.Run([<InlineIfLambda>] runExpr: ComponentBuilderCode) =
            let builder = ComponentBuilder(builder)
            runExpr builder

    // TODO: Fragment

    type AttributeBlock() =

        member inline _.Zero() : ComponentBuilderCode = fun _ -> ()

        member inline _.Yield((data, value: 'a)) : ComponentBuilderCode =
            fun builder ->
                builder.AddAttribute(data, value)

        member inline this.Run([<InlineIfLambda>] runExpr: ComponentBuilderCode) : ComponentBuilderCode =
            fun builder ->
                runExpr builder

    [<Sealed>]
    type ComponentBlock<'a when 'a :> ComponentBase>() =
        inherit ComponentBlockBase()

        member inline this.Run([<InlineIfLambda>] runExpr: ComponentBuilderCode) : ComponentBuilderCode =
            fun builder ->
                builder.OpenComponent<'a>()
                runExpr builder
                builder.CloseComponent()

    [<AbstractClass; Sealed>]
    type ComponentCodeCache<'a when 'a :> ComponentBase>() =
        static member val Instance = ComponentBlock<'a>()

    [<AbstractClass>]
    type ElementBlockBase() =
        inherit ComponentBlockBase()

        abstract member Name: string

        member inline this.Run([<InlineIfLambda>] runExpr: ComponentBuilderCode) : ComponentBuilderCode =
            fun builder ->
                builder.OpenElement(this.Name)
                runExpr builder
                builder.CloseElement()

    [<Sealed>]
    type Div() =
        inherit ElementBlockBase()
        override this.Name = "div"

    [<Sealed>]
    type H3() =
        inherit ElementBlockBase()
        override this.Name = "h3"

[<AutoOpen>]
module ComponentBuilder =
    open ComponentBuilderImpl

    let (=.) (attr: string) (value: 'a) = Attribute<'a>(attr, value)

    let html builder = ComponentBuilderRunner(builder)
    let div = Div()
    let h3 = H3()
    let c<'a when 'a :> ComponentBase> = ComponentCodeCache<'a>.Instance
    let attr(name, value) = Attribute(name, value)
    let style = AttributeName<string>("style")
    let class' = AttributeName<string>("class")
    let type' = AttributeName<string>("type")