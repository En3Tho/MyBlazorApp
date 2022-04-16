module MyBlazorApp.Services.DiscriminatedUnions.Contracts.Version1

open System.Threading.Tasks

let [<Literal>] Version = "v1"

module Routes =
    let [<Literal>] Controller = "discriminated-unions" + "/" + Version + "/"
    let [<Literal>] GetRandomImportantData = "get-random-important-data"

module Endpoints =
    let [<Literal>] GetRandomImportantData = Routes.Controller + Routes.GetRandomImportantData

type ImportantDataDto =
    | NameAndAge of Name: string * Age: int
    | PriceRangeAndCount of RangeFrom: int * RangeTo: int * Count: int
    | Cart of Items: string[]

type IDiscriminatedUnionsService =
    abstract GetRandomImportantData: unit -> ValueTask<ImportantDataDto>