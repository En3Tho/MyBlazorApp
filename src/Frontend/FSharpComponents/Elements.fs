module [<AutoOpen>] En3Tho.FSharp.ComputationExpressions.BlazorBuilder.Elements

open En3Tho.FSharp.ComputationExpressions.BlazorBuilder.Core
open En3Tho.FSharp.ComputationExpressions.BlazorBuilder.Core.KnownElements

let div = ElementBlockBase<Div>()
let span = ElementBlockBase<Span>()
let input = ElementBlockBase<Input>()
let button = ElementBlockBase<Button>()
let h1 = ElementBlockBase<H1>()
let h2 = ElementBlockBase<H2>()
let h3 = ElementBlockBase<H3>()