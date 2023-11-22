namespace MyBlazorApp.Services.DiscriminatedUnions.Contracts.V1

open System.Threading.Tasks

module Endpoints =

    let [<Literal>] ServiceName = "discriminated-unions"
    let [<Literal>] ServiceDiscoveryUrl = "http://" + ServiceName
    let [<Literal>] GetRandomImportantData = ServiceName + "/v1/" + "get-random-important-data"

type ImportantDataDto =
    | NameAndAge of Name: string * Age: int
    | PriceRangeAndCount of RangeFrom: int * RangeTo: int * Count: int
    | Cart of Items: string[]

type IDiscriminatedUnionsServiceV1 =
    abstract GetRandomImportantData: unit -> ValueTask<ImportantDataDto>