namespace MyBlazorApp.Services.DiscriminatedUnions.Client

open System.Threading.Tasks

[<AutoOpen>]
module Version1 =

    let [<Literal>] Version = "v1"

    module Routes =
        let [<Literal>] ServiceName = "discriminated-unions" + "/" + Version + "/"
        let [<Literal>] GetRandomImportantData = "get-random-important-data"

    module Endpoints =
        let [<Literal>] GetRandomImportantData = Routes.ServiceName + Routes.GetRandomImportantData

type ImportantDataDto =
    | NameAndAge of Name: string * Age: int
    | PriceRangeAndCount of RangeFrom: int * RangeTo: int * Count: int
    | Cart of Items: string[]

type IDiscriminatedUnionsServiceV1 =
    abstract GetRandomImportantData: unit -> ValueTask<ImportantDataDto>