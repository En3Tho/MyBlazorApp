module [<Microsoft.FSharp.Core.AutoOpen>] En3Tho.FSharp.BlazorBuilder.Runners

open En3Tho.FSharp.BlazorBuilder.Core
open Microsoft.AspNetCore.Components
open Microsoft.AspNetCore.Components.Rendering
open Microsoft.FSharp.Core

[<AbstractClass>]
type FSharpComponentBase() =
    inherit ComponentBase()

    abstract member BuildRenderTreeCore: builder: BlazorBuilderCore -> unit

    override this.BuildRenderTree(builder: RenderTreeBuilder) =
        this.BuildRenderTreeCore(BlazorBuilderCore(builder))

type RenderTreeBuilder with
    member inline this.Render([<InlineIfLambda>] runExpr) =
        runExpr this

type BlazorBuilderCore with
    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderMarkupCode) =
        runExpr.Invoke this

let blazor = BlazorBuilderRunner()
let fragment = RenderFragmentRunner()

[<AutoOpen; AbstractClass; Sealed>]
type AttributeFunctions =
    static member attr<'a>(name, value) = CustomAttribute<'a>(name, value)
    static member attr(name) = CustomAttribute(name)

let (=>) (name: string) (value: 'a) = CustomAttribute<'a>(name, value)
let markup markupString = MarkupString(markupString)

// TODO: component block -> struct
// this will create a struct instread
let render<'a when 'a :> ComponentBase> = ComponentBlock<'a>.Instance
let inline getBuilder() = GetBuilderIntrinsic()