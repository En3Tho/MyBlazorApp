namespace En3Tho.FSharp.BlazorBuilder

open System
open System.Globalization
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open System.Threading.Tasks
open En3Tho.FSharp.BlazorBuilder.Core
open En3Tho.FSharp.BlazorBuilder.Core.KnownAttributes
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


    static member onInput' (receiver: obj, value: Action) =
        mk<OnInput, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onInput' (receiver: obj, value: Action<ChangeEventArgs>) =
        mk<OnInput, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onInput' (receiver: obj, value: EventCallback) =
        mk<OnInput, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onInput' (receiver: obj, value: EventCallback<ChangeEventArgs>) =
        mk<OnInput, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onInput' (receiver: obj, value: Func<Task>) =
        mk<OnInput, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onInput' (receiver: obj, value: Func<ChangeEventArgs, Task>) =
        mk<OnInput, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))


    static member onKeyDown' (receiver: obj, value: Action) =
        mk<OnKeyDown, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyDown' (receiver: obj, value: Action<KeyboardEventArgs>) =
        mk<OnKeyDown, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyDown' (receiver: obj, value: EventCallback) =
        mk<OnKeyDown, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyDown' (receiver: obj, value: EventCallback<KeyboardEventArgs>) =
        mk<OnKeyDown, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyDown' (receiver: obj, value: Func<Task>) =
        mk<OnKeyDown, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyDown' (receiver: obj, value: Func<KeyboardEventArgs, Task>) =
        mk<OnKeyDown, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))