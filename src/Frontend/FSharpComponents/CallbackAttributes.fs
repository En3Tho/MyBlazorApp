namespace En3Tho.FSharp.ComputationExpressions.BlazorBuilder

open System
open System.Globalization
open System.Runtime.InteropServices
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.BlazorBuilder.Core
open En3Tho.FSharp.ComputationExpressions.BlazorBuilder.Core.KnownAttributes
open Microsoft.AspNetCore.Components
open Microsoft.AspNetCore.Components.Web

[<AbstractClass; Sealed; AutoOpen>]
type CallbackAttributes() =
    // TODO: find a way to inline this thing to infer type automatically ?
    // Or just write a generator, whatever?
    static member private mk<'a, 'b when 'a :> IAttributeName and 'a: struct> value = Attribute<'a, EventCallback<'b>>(value)

    static member onClick' (receiver: obj, value: Action) =
        mk<OnClick, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onClick' (receiver: obj, value: Action<MouseEventArgs>) =
        mk<OnClick, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onClick' (receiver: obj, value: EventCallback) =
        mk<OnClick, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onClick' (receiver: obj, value: EventCallback<MouseEventArgs>) =
        mk<OnClick, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onClick' (receiver: obj, value: Func<Task>) =
        mk<OnClick, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onClick' (receiver: obj, value: Func<MouseEventArgs, Task>) =
        mk<OnClick, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onChange' (receiver: obj, value: Action) =
        mk<OnChange, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onChange' (receiver: obj, value: Action<ChangeEventArgs>) =
        mk<OnChange, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onChange' (receiver: obj, value: EventCallback) =
        mk<OnChange, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onChange' (receiver: obj, value: EventCallback<ChangeEventArgs>) =
        mk<OnChange, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onChange' (receiver: obj, value: Func<Task>) =
        mk<OnChange, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onChange' (receiver: obj, value: Func<ChangeEventArgs, Task>) =
        mk<OnChange, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    // TODO: SetUpdatesAttributeName
    // Bind can be a special struct I guess?
    static member bind' (receiver: obj, existingValue, onChange: Action<'a>, [<Optional; DefaultParameterValue(null: CultureInfo)>] culture) =
        struct (
            Attribute<Value,_>(BindConverter.FormatValue<'a>(existingValue, culture)),
            mk<OnChange, _>(EventCallback.Factory.CreateBinder(receiver, onChange, existingValue, culture))
        )

    static member bind' (receiver: obj, existingValue, onChange: Func<'a, Task>, [<Optional; DefaultParameterValue(null: CultureInfo)>] culture) =
        struct (
            Attribute<Value,_>(BindConverter.FormatValue<'a>(existingValue, culture)),
            mk<OnChange, _>(EventCallback.Factory.CreateBinder(receiver, onChange, existingValue, culture))
        )

    static member bind' (receiver: obj, existingValue: int, onChange: Action<int>, [<Optional; DefaultParameterValue(null: CultureInfo)>] culture) =
        struct (
            Attribute<Value,_>(BindConverter.FormatValue(existingValue, culture)),
            mk<OnChange, _>(EventCallback.Factory.CreateBinder(receiver, onChange, existingValue, culture))
        )

    static member bind' (receiver: obj, existingValue: int, onChange: Func<int, Task>, [<Optional; DefaultParameterValue(null: CultureInfo)>] culture) =
        struct (
            Attribute<Value,_>(BindConverter.FormatValue(existingValue, culture)),
            mk<OnChange, _>(EventCallback.Factory.CreateBinder(receiver, onChange, existingValue, culture))
        )