﻿namespace MyBlazorApp.BlazorClient.Backend.Models

open System
open System.Collections.Generic
open Microsoft.AspNetCore.Components
open Microsoft.Extensions.Logging
open MyBlazorApp.Utility.FSharpHelpers
open MyBlazorApp.Utility.Modules

[<AbstractClass>]
type private ComponentValueDictionary<'TType, 'TKey, 'TValue when 'TKey: equality>() =

    [<DefaultValue>]
    static val mutable private _KeyValueBag: Dictionary<'TKey, 'TValue>

    static do ComponentValueDictionary<'TType, 'TKey, 'TValue>._KeyValueBag <- Dictionary()

    static member KeyValueBag =
        ComponentValueDictionary<'TType, 'TKey, 'TValue>
            ._KeyValueBag

type ComponentDataProvider(_logger: ILogger<ComponentDataProvider>) =
    member this.GetOrCreateNew<'TType, 'TKey, 'TValue when 'TKey: equality and 'TValue: (new: unit -> 'TValue)> key =
        let bag = ComponentValueDictionary<'TType, 'TKey, 'TValue>.KeyValueBag
        
        match bag |> Dictionary.tryGetValue key with
        | Some value -> value
        | None ->
            let value = Object.createNew<'TValue> ()
            bag.[key] <- value
            value

    member this.GetOrCreateWith<'TType, 'TKey, 'TValue when 'TKey: equality> key (factory: 'TValue Func) =
        let bag = ComponentValueDictionary<'TType, 'TKey, 'TValue>.KeyValueBag
        
        match bag |> Dictionary.tryGetValue key with
        | Some value -> value
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

[<Struct>]
type ComponentDataChangeEventHandler =
    val mutable private _handler: EventHandler
    val mutable private _componentDatas: ComponentData[]

    member this.Subscribe(action: Action, [<ParamArray>] datas: ComponentData[]) =
        this._handler <- EventHandler(fun _ _ -> action.Invoke())
        this._componentDatas <- datas
        for data in datas do data.OnChange.AddHandler this._handler

    member this.Subscribe(action: Action, data1) =
        this._handler <- EventHandler(fun _ _ -> action.Invoke())
        this._componentDatas <- [| data1 |]
        data1.OnChange.AddHandler this._handler

    member this.Subscribe(action: Action, data1, data2) =
        this._handler <- EventHandler(fun _ _ -> action.Invoke())
        this._componentDatas <- [| data1; data2 |]
        data1.OnChange.AddHandler this._handler
        data2.OnChange.AddHandler this._handler

    member this.Subscribe(action: Action, data1, data2, data3) =
        this._handler <- EventHandler(fun _ _ -> action.Invoke())
        this._componentDatas <- [| data1; data2; data3 |]
        data1.OnChange.AddHandler this._handler
        data2.OnChange.AddHandler this._handler
        data3.OnChange.AddHandler this._handler
    
    member this.Unsubscribe() =
        if this._componentDatas <> null then
            for data in this._componentDatas do
                data.OnChange.RemoveHandler this._handler
        this._handler <- null
        this._componentDatas <- null

type CounterData() =
    inherit ComponentData()
    let mutable current = 0
    let mutable totalClicks = 0

    member this.Current
        with get () = current
        and set (value) =
            current <- value
            this.OnDataChanged()

    member this.TotalClicks
        with get () = totalClicks
        and set (value) =
            totalClicks <- value
            this.OnDataChanged()

type Theme =
    | Black = 0
    | Red = 1
    | Blue = 2

type ThemeSwitch(theme: Theme) =
    inherit ComponentData()
    let mutable theme = theme

    member this.Theme
        with get () = theme
        and set (value) =
            theme <- value
            this.OnDataChanged()

    member this.ThemeString =
        match this.Theme with
        | Theme.Black -> "black-Theme"
        | Theme.Blue -> "blue-Theme"
        | Theme.Red -> "red-Theme"
        | _ -> ""
