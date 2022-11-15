module [<AutoOpen>] En3Tho.FSharp.BlazorBuilder.Elements

open En3Tho.FSharp.BlazorBuilder.Core.KnownElements

let div = ElementBlockBase<Div>()
let span = ElementBlockBase<Span>()
let input = ElementBlockBase<Input>()
let button = ElementBlockBase<Button>()
let table = ElementBlockBase<Table>()
let tr = ElementBlockBase<Tr>()
let td = ElementBlockBase<Td>()
let h1 = ElementBlockBase<H1>()
let h2 = ElementBlockBase<H2>()
let h3 = ElementBlockBase<H3>()
let colgroup = ElementBlockBase<Colgroup>()
let col = ElementBlockBase<Col>()
