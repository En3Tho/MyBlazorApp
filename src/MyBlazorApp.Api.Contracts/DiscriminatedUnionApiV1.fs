module MyBlazorApp.Api.Contracts.DiscriminatedUnionApiV1

open System.Threading.Tasks
open MyBlazorApp.Domain.DiscriminatedUnionService

let [<Literal>] Version = "v1"

module Routes =
    let [<Literal>] Controller = "discriminatedunion" + "/" + Version + "/"
    let [<Literal>] GetRandomImportantData = "getrandomimportantdata"

module Endpoints =
    let [<Literal>] GetRandomImportantData = Routes.Controller + Routes.GetRandomImportantData

type IService =
    abstract GetRandomImportantData: unit -> ImportantData Task