namespace MyBlazorApp.BlazorClient.Backend.Models

open System
open System.Collections.Generic
open Microsoft.Extensions.Logging
open MyBlazorApp.Utility.FSharpHelpers
open MyBlazorApp.Utility.Modules

[<AbstractClass>]
type private ComponentValueDictionary<'TType, 'TKey, 'TValue when 'TKey: equality>() =
    static let Bag = Dictionary<'TKey, 'TValue>()

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
            let value = Object.createNew<'TValue>
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
    val mutable private _componentsData: ComponentData[]

    member this.Subscribe(action: Action, [<ParamArray>] componentsData: ComponentData[]) =
        this._handler <- EventHandler(fun _ _ -> action.Invoke())
        this._componentsData <- componentsData
        for data in componentsData do data.OnChange.AddHandler this._handler

    member this.Subscribe(action: Action, data1) = this.Subscribe(action, [| data1 |])

    member this.Subscribe(action: Action, data1, data2) = this.Subscribe(action, [| data1; data2 |])

    member this.Subscribe(action: Action, data1, data2, data3) = this.Subscribe(action, [| data1; data2; data3 |])
    
    member this.Unsubscribe() =
        if this._componentsData <> null then
            for data in this._componentsData do
                data.OnChange.RemoveHandler this._handler
        this._handler <- null
        this._componentsData <- null

type CounterData() =
    inherit ComponentData()
    let mutable current = 0
    let mutable totalClicks = 0

    member this.Current
        with get () = current
         and set value =
            current <- value
            this.OnDataChanged()

    member this.TotalClicks
        with get () = totalClicks
         and set value =
            totalClicks <- value
            this.OnDataChanged()

type SimpleChatData() =
    inherit ComponentData()
    let messages = List<string>()

    member val CurrentMessage: string = null with get, set
    member _.Messages = messages

    member this.AddMessage msg =
        messages.Add msg
        this.OnDataChanged()

type SingleValueData<'a>() =
    inherit ComponentData()
    let mutable value: 'a = Unchecked.defaultof<_>
    member this.Value
        with get() = value
         and set newValue =
            value <- newValue
            this.OnDataChanged()

type Theme =
    | Custom = 0
    | Black = 1
    | Red = 2
    | Blue = 3

type ThemeSwitch(theme: Theme) =
    inherit ComponentData()
    let mutable theme = theme

    static member ThemesCount = Enum.GetValues<Theme>().Length

    member this.Theme
        with get () = theme
         and set value =
            theme <- value
            this.OnDataChanged()

    member this.Next =
        (int this.Theme + 1) % ThemeSwitch.ThemesCount
        |> enum<Theme>

    member this.ThemeString =
        match this.Theme with
        | Theme.Black -> "black-Theme"
        | Theme.Blue -> "blue-Theme"
        | Theme.Red -> "red-Theme"
        | Theme.Custom -> "custom-Theme"
        | _ -> "blue-Theme"