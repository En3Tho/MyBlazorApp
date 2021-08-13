module MyBlazorApp.Services.DiscriminatedUnions.Contracts.Version1

open System.Threading.Tasks
open MyBlazorApp.Services.DiscriminatedUnions.Domain

let [<Literal>] Version = "v1"

module Routes =
    let [<Literal>] Controller = "discriminated-union" + "/" + Version + "/"
    let [<Literal>] GetRandomImportantData = "get-random-important-data"

module Endpoints =
    let [<Literal>] GetRandomImportantData = Routes.Controller + Routes.GetRandomImportantData

type IDiscriminatedUnionsService =
    abstract GetRandomImportantData: unit -> ValueTask<ImportantData>