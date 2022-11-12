module [<Microsoft.FSharp.Core.AutoOpen>] En3Tho.FSharp.ComputationExpressions.BlazorBuilder.Runner

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
let attr<'a>(name, value) = CustomAttribute<'a>(name, value)
let (=>) (name: string) (value: 'a) = CustomAttribute<'a>(name, value)
let markup markupString = MarkupString(markupString)
let component<'a when 'a :> ComponentBase> = ComponentBlock<'a>.Instance
let c<'a when 'a :> ComponentBase> = ComponentBlock<'a>.Instance