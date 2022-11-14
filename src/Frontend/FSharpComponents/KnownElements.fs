module En3Tho.FSharp.ComputationExpressions.BlazorBuilder.Core.KnownElements

type [<Struct>] Div =
    interface IElementName with
        member _.Name = "div"

type [<Struct>] Span =
    interface IElementName with
        member _.Name = "span"

type [<Struct>] Button =
    interface IElementName with
        member _.Name = "button"

type [<Struct>] Input =
    interface IElementName with
        member _.Name = "input"

type [<Struct>] Table =
    interface IElementName with
        member _.Name = "table"

type [<Struct>] Tr =
    interface IElementName with
        member _.Name = "tr"

type [<Struct>] Td =
    interface IElementName with
        member _.Name = "td"

type [<Struct>] H1 =
    interface IElementName with
        member _.Name = "h1"

type [<Struct>] H2 =
    interface IElementName with
        member _.Name = "h2"

type [<Struct>] H3 =
    interface IElementName with
        member _.Name = "h3"