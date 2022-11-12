namespace En3Tho.FSharp.ComputationExpressions.ComponentBuilder

open System.Runtime.CompilerServices
open En3Tho.FSharp.Extensions.GenericBuilderBase
open Microsoft.AspNetCore.Components
open Microsoft.AspNetCore.Components.Rendering
open Microsoft.FSharp.Core

module rec ComponentBuilderImpl =

    type ComponentBuilder(builder: RenderTreeBuilder) =
        let mutable sequenceCount = 0

        member _.Builder = builder

        member this.AddMarkup(content: string) =
            builder.AddMarkupContent(sequenceCount, content)
            sequenceCount <- sequenceCount + 1

        member _.OpenComponent<'a>() =
            builder.OpenComponent(sequenceCount, typeof<'a>)
            sequenceCount <- sequenceCount + 1

        member _.OpenElement(name: string) =
            builder.OpenElement(sequenceCount, name)
            sequenceCount <- sequenceCount + 1

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

    type RenderBlockBase() =
        inherit UnitBuilderBase<ComponentBuilder>()

        member inline _.Yield([<InlineIfLambda>] codeBuilderCode: ComponentBuilderCode) : ComponentBuilderCode =
            fun builder ->
                codeBuilderCode builder

        member inline _.Yield(codeBuilderCode: RenderFragment) : ComponentBuilderCode =
            fun builder ->
                codeBuilderCode.Invoke(builder.Builder)

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
    type ComponentBuilderRunner() =

        inherit RenderBlockBase()
        member inline this.Run([<InlineIfLambda>] runExpr: ComponentBuilderCode) =
            fun builder ->
                let builder = ComponentBuilder(builder)
                runExpr builder

    [<Sealed>]
    type RenderFragmentRunner() =

        inherit RenderBlockBase()
        member inline this.Run([<InlineIfLambda>] runExpr: ComponentBuilderCode) : RenderFragment =
            RenderFragment(fun builder -> runExpr (ComponentBuilder(builder)))

    [<Sealed>]
    type ComponentBlock<'a when 'a :> ComponentBase>() =
        inherit RenderBlockBase()

        static member val Instance = ComponentBlock<'a>()

        member inline this.Run([<InlineIfLambda>] runExpr: ComponentBuilderCode) : ComponentBuilderCode =
            fun builder ->
                builder.OpenComponent<'a>()
                runExpr builder
                builder.CloseComponent()

    [<AbstractClass>]
    type ElementBlockBase() =
        inherit RenderBlockBase()

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
    type Span() =
        inherit ElementBlockBase()
        override this.Name = "span"

    [<Sealed>]
    type H1() =
        inherit ElementBlockBase()
        override this.Name = "h3"

    [<Sealed>]
    type H2() =
        inherit ElementBlockBase()
        override this.Name = "h3"

    [<Sealed>]
    type H3() =
        inherit ElementBlockBase()
        override this.Name = "h3"

    [<Sealed>]
    type H4() =
        inherit ElementBlockBase()
        override this.Name = "h4"

    [<Sealed>]
    type H5() =
        inherit ElementBlockBase()
        override this.Name = "h5"

    [<Sealed>]
    type H6() =
        inherit ElementBlockBase()
        override this.Name = "h6"

    [<Sealed>]
    type P() =
        inherit ElementBlockBase()
        override this.Name = "p"

    [<Sealed>]
    type Ul() =
        inherit ElementBlockBase()
        override this.Name = "ul"

    [<Sealed>]
    type Ol() =
        inherit ElementBlockBase()
        override this.Name = "ol"

    [<Sealed>]
    type Li() =
        inherit ElementBlockBase()
        override this.Name = "li"

    [<Sealed>]
    type A() =
        inherit ElementBlockBase()
        override this.Name = "a"

    [<Sealed>]
    type Img() =
        inherit ElementBlockBase()
        override this.Name = "img"

    [<Sealed>]
    type Table() =
        inherit ElementBlockBase()
        override this.Name = "table"

    [<Sealed>]
    type THead() =
        inherit ElementBlockBase()
        override this.Name = "thead"

    [<Sealed>]
    type TBody() =
        inherit ElementBlockBase()
        override this.Name = "tbody"

    [<Sealed>]
    type TFoot() =
        inherit ElementBlockBase()
        override this.Name = "tfoot"

    [<Sealed>]
    type Tr() =
        inherit ElementBlockBase()
        override this.Name = "tr"

    [<Sealed>]
    type Th() =
        inherit ElementBlockBase()
        override this.Name = "th"

    [<Sealed>]
    type Td() =
        inherit ElementBlockBase()
        override this.Name = "td"

    [<Sealed>]
    type Form() =
        inherit ElementBlockBase()
        override this.Name = "form"

    [<Sealed>]
    type Input() =
        inherit ElementBlockBase()
        override this.Name = "input"

    [<Sealed>]
    type Label() =
        inherit ElementBlockBase()
        override this.Name = "label"

    [<Sealed>]
    type Button() =
        inherit ElementBlockBase()
        override this.Name = "button"

    [<Sealed>]
    type Select() =
        inherit ElementBlockBase()
        override this.Name = "select"

    [<Sealed>]
    type Option() =
        inherit ElementBlockBase()
        override this.Name = "option"

    [<Sealed>]
    type TextArea() =
        inherit ElementBlockBase()
        override this.Name = "textarea"

    [<Sealed>]
    type FieldSet() =
        inherit ElementBlockBase()
        override this.Name = "fieldset"

    [<Sealed>]
    type Legend() =
        inherit ElementBlockBase()
        override this.Name = "legend"

    [<Sealed>]
    type Dl() =
        inherit ElementBlockBase()
        override this.Name = "dl"

    [<Sealed>]
    type Dt() =
        inherit ElementBlockBase()
        override this.Name = "dt"

    [<Sealed>]
    type Dd() =
        inherit ElementBlockBase()
        override this.Name = "dd"

[<AutoOpen>]
module ComponentBuilder =
    open ComponentBuilderImpl

    let html = ComponentBuilderRunner()
    let fragment = RenderFragmentRunner()
    let c<'a when 'a :> ComponentBase> = ComponentBlock<'a>.Instance

    // Todo: these should actually be defined in CSharp library to utilize static readonly fields?
    // or a completely different mechanism?

    let div = Div()
    let span = Span()
    let h1 = H1()
    let h2 = H2()
    let h3 = H3()
    let h4 = H4()
    let h5 = H5()
    let h6 = H6()
    let p = P()
    let ul = Ul()
    let ol = Ol()
    let li = Li()
    let a = A()
    let img = Img()
    let table = Table()
    let thead = THead()
    let tbody = TBody()
    let tfoot = TFoot()
    let tr = Tr()
    let th = Th()
    let td = Td()
    let form = Form()
    let input = Input()
    let label = Label()
    let button = Button()
    let select = Select()
    let option = Option()
    let textarea = TextArea()
    let fieldset = FieldSet()
    let legend = Legend()
    let dl = Dl()
    let dt = Dt()
    let dd = Dd()

    let attr(name, value) = Attribute(name, value)
    let style' = AttributeName<string>("style")
    let class' = AttributeName<string>("class")
    let type' = AttributeName<string>("type")
    let id' = AttributeName<string>("id")
    let name' = AttributeName<string>("name")
    let value' = AttributeName<string>("value")
    let placeholder' = AttributeName<string>("placeholder")
    let href' = AttributeName<string>("href")
    let src' = AttributeName<string>("src")
    let alt' = AttributeName<string>("alt")