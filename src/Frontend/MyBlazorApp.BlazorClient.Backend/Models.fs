namespace MyBlazorApp.BlazorClient.Backend.Models

open System
open System.Collections.Generic
open System.Runtime.InteropServices
open Microsoft.Extensions.Logging
open MyBlazorApp.Services.DiscriminatedUnions.Contracts.Version1
open MyBlazorApp.Services.WeatherForecasts.Contracts.Version1
open MyBlazorApp.Utility.FSharpHelpers
open En3Tho.FSharp.Extensions

type ImportantData =
    | NameAndAge of Name: string * Age: int
    | PriceRangeAndCount of RangeFrom: int * RangeTo: int * Count: int
    | Cart of Items: string[]

module ImportantDataMapper =
    let fromDto (dto: ImportantDataDto) =
        match dto with
        | ImportantDataDto.NameAndAge(name, age) -> NameAndAge(name, age)
        | ImportantDataDto.PriceRangeAndCount(priceFrom, priceTo, count) -> PriceRangeAndCount(priceFrom, priceTo, count)
        | ImportantDataDto.Cart(items) -> Cart(items)

type WeatherForecast = {
    Date: DateTime
    TemperatureC: int
    Summary: string
} with
    member this.TemperatureF = 32 + int (float this.TemperatureC / 0.5556);

module WeatherForecastMapper =
    let fromDto (dto: WeatherForecastDto) : WeatherForecast = {
        Date = dto.Date
        TemperatureC = dto.TemperatureC
        Summary = dto.Summary
    }

/// Single threaded component data storage
type ComponentDataStorage(_logger: ILogger<ComponentDataStorage>) =

    let typeStorage = Dictionary<struct (Type * Type), obj>() // stores Dictionary<'TKey, 'TValue> as obj

    member this.GetValueStorage<'TType, 'TKey, 'TValue when 'TKey: equality>() =
        let typeKey = struct (typeof<'TType>, typeof<'TKey>)
        let mutable found = false
        let valueStorageRef = &CollectionsMarshal.GetValueRefOrAddDefault(typeStorage, typeKey, &found)
        if not found then valueStorageRef <- Dictionary<'TKey, 'TValue>()
        valueStorageRef :?> Dictionary<'TKey, 'TValue>

    member this.GetOrCreateNew<'TType, 'TKey, 'TValue when 'TKey: equality and 'TValue: (new: unit -> 'TValue)> (key: 'TKey) =
        let valueStorage = this.GetValueStorage<'TType, 'TKey, 'TValue>()
        let mutable found = false
        let valueRef = &CollectionsMarshal.GetValueRefOrAddDefault(valueStorage, key, &found)
        if not found then valueRef <- new 'TValue()
        valueRef

    member this.GetOrCreateWith<'TType, 'TKey, 'TValue when 'TKey: equality> (key: 'TKey) (factory: 'TValue Func) =
        let valueStorage = this.GetValueStorage<'TType, 'TKey, 'TValue>()
        let mutable found = false
        let valueRef = &CollectionsMarshal.GetValueRefOrAddDefault(valueStorage, key, &found)
        if not found then valueRef <- factory.Invoke()
        valueRef

    member this.Set<'TType, 'TKey, 'TValue when 'TKey: equality> (key: 'TKey) (value: 'TValue) =
        let valueStorage = this.GetValueStorage<'TType, 'TKey, 'TValue>()
        valueStorage.[key] <- value

    member this.Remove<'TType, 'TKey, 'TValue when 'TKey: equality> (key: 'TKey) =
        let valueStorage = this.GetValueStorage<'TType, 'TKey, 'TValue>()
        valueStorage.Remove key |> ignore

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