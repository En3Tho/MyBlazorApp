module [<Microsoft.FSharp.Core.AutoOpen>] En3Tho.FSharp.ComputationExpressions.BlazorBuilder.Runners

open En3Tho.FSharp.ComputationExpressions.BlazorBuilder.Core
open Microsoft.AspNetCore.Components
open Microsoft.AspNetCore.Components.Rendering
open Microsoft.FSharp.Core

type RenderTreeBuilder with

    member inline this.Render([<InlineIfLambda>] runExpr) =
        runExpr this

let blazor = BlazorBuilderRunner()
let fragment = RenderFragmentRunner()
let attributes = AttributeBlock()
let attrs = attributes

[<AutoOpen; AbstractClass; Sealed>]
type AttributeFunctions =
    static member attr<'a>(name, value) = CustomAttribute<'a>(name, value)
    static member attr(name) = CustomAttribute(name)

let (=>) (name: string) (value: 'a) = CustomAttribute<'a>(name, value)
let markup markupString = MarkupString(markupString)
let component<'a when 'a :> ComponentBase> = ComponentBlock<'a>.Instance
let c<'a when 'a :> ComponentBase> = ComponentBlock<'a>.Instance