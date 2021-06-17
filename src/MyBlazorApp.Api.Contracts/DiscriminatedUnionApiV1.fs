module MyBlazorApp.Api.Contracts.DiscriminatedUnionApiV1

open System.Threading.Tasks
open MyBlazorApp.Domain.DiscriminatedUnionService

let [<Literal>] Version = "v1"

module Routes =
    let [<Literal>] Controller = "discriminated-union" + "/" + Version + "/"
    let [<Literal>] GetRandomImportantData = "get-random-important-data"

module Endpoints =
    let [<Literal>] GetRandomImportantData = Routes.Controller + Routes.GetRandomImportantData

type IService =
    abstract GetRandomImportantData: unit -> ImportantData Task