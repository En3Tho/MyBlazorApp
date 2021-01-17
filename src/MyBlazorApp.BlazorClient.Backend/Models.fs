namespace MyBlazorApp.BlazorClient.Backend.Models

open System
open System.Collections.Generic
open Microsoft.Extensions.Logging
open MyBlazorApp.Utility.FSharpHelpers
open MyBlazorApp.Utility.Modules

[<AbstractClass>]
type private ComponentValueDictionary<'TType, 'TKey, 'TValue when 'TKey: equality>() =

    [<DefaultValue>]
    static val mutable private _KeyValueBag: Dictionary<'TKey, 'TValue>
    static do ComponentValueDictionary<'TType, 'TKey, 'TValue>._KeyValueBag <- Dictionary()
    static member KeyValueBag = ComponentValueDictionary<'TType, 'TKey, 'TValue>._KeyValueBag

    static member val KeyValueBag2 = Dictionary<'TKey, 'TValue>() with get

type ComponentDataProvider(_logger: ILogger<ComponentDataProvider>) =
    member this.GetOrCreateNew<'TType, 'TKey, 'TValue when 'TKey: equality and 'TValue: (new: unit -> 'TValue)> key =
        let bag = ComponentValueDictionary<'TType, 'TKey, 'TValue>.KeyValueBag
        match bag |> Dictionary.tryGetValue key with
        | Some value ->
            value
        | None ->
            let value = Object.createNew<'TValue>()
            bag.[key] <- value
            value

    member this.GetOrCreateWith<'TType, 'TKey, 'TValue when 'TKey: equality> key (factory: 'TValue Func) =
        let bag = ComponentValueDictionary<'TType, 'TKey, 'TValue>.KeyValueBag
        match bag |> Dictionary.tryGetValue key with
        | Some value ->
            value
        | None ->
            let value = factory.Invoke()
            bag.[key] <- value
            value

    member this.Set<'TType, 'TKey, 'TValue when 'TKey: equality> key value =
        let bag = ComponentValueDictionary<'TType, 'TKey, 'TValue>.KeyValueBag
        bag.[key] <- value

    member this.Remove<'TType, 'TKey, 'TValue when 'TKey: equality> key =
        let bag = ComponentValueDictionary<'TType, 'TKey, 'TValue>.KeyValueBag
        bag.Remove key |> ignore

type CounterData() =
    inherit ComponentData()
    let mutable current = 0
    let mutable totalClicks = 0
    member this.Current with get() = current and set (value) = current <- value; this.OnDataChanged()
    member this.TotalClicks with get() = totalClicks and set (value) = totalClicks <- value; this.OnDataChanged()