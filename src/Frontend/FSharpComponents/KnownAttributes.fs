module En3Tho.FSharp.ComputationExpressions.BlazorBuilder.Core.KnownAttributes

type [<Struct>] Style =
    interface IAttributeName with
        member _.Name = "style"

type [<Struct>] Class =
    interface IAttributeName with
        member _.Name = "class"

type [<Struct>] Type =
    interface IAttributeName with
        member _.Name = "type"

type [<Struct>] Value =
    interface IAttributeName with
        member _.Name = "value"

type [<Struct>] OnClick =
    interface IAttributeName with
        member _.Name = "onclick"

type [<Struct>] OnChange =
    interface IAttributeName with
        member _.Name = "onchange"