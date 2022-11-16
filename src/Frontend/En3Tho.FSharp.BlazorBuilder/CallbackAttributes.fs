namespace En3Tho.FSharp.BlazorBuilder

open System
open System.Threading.Tasks
open En3Tho.FSharp.BlazorBuilder.Core
open En3Tho.FSharp.BlazorBuilder.Core.KnownAttributes
open Microsoft.AspNetCore.Components
open Microsoft.AspNetCore.Components.Web

[<AbstractClass; Sealed; AutoOpen>]
type CallbackAttributes() =
    // TODO: find a way to inline this thing to infer type automatically ?
    // Or just write a generator, whatever?

    static member onClick' (receiver: obj, value: Action) =
        Attribute<OnClick, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onClick' (receiver: obj, value: Action<MouseEventArgs>) =
        Attribute<OnClick, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onClick' (receiver: obj, value: EventCallback) =
        Attribute<OnClick, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onClick' (receiver: obj, value: EventCallback<MouseEventArgs>) =
        Attribute<OnClick, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onClick' (receiver: obj, value: Func<Task>) =
        Attribute<OnClick, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onClick' (receiver: obj, value: Func<MouseEventArgs, Task>) =
        Attribute<OnClick, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))


    static member onChange' (receiver: obj, value: Action) =
        Attribute<OnChange, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onChange' (receiver: obj, value: Action<ChangeEventArgs>) =
        Attribute<OnChange, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onChange' (receiver: obj, value: EventCallback) =
        Attribute<OnChange, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onChange' (receiver: obj, value: EventCallback<ChangeEventArgs>) =
        Attribute<OnChange, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onChange' (receiver: obj, value: Func<Task>) =
        Attribute<OnChange, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onChange' (receiver: obj, value: Func<ChangeEventArgs, Task>) =
        Attribute<OnChange, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))


    static member onInput' (receiver: obj, value: Action) =
        Attribute<OnInput, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onInput' (receiver: obj, value: Action<ChangeEventArgs>) =
        Attribute<OnInput, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onInput' (receiver: obj, value: EventCallback) =
        Attribute<OnInput, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onInput' (receiver: obj, value: EventCallback<ChangeEventArgs>) =
        Attribute<OnInput, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onInput' (receiver: obj, value: Func<Task>) =
        Attribute<OnInput, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onInput' (receiver: obj, value: Func<ChangeEventArgs, Task>) =
        Attribute<OnInput, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))


    static member onKeyDown' (receiver: obj, value: Action) =
        Attribute<OnKeyDown, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyDown' (receiver: obj, value: Action<KeyboardEventArgs>) =
        Attribute<OnKeyDown, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyDown' (receiver: obj, value: EventCallback) =
        Attribute<OnKeyDown, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyDown' (receiver: obj, value: EventCallback<KeyboardEventArgs>) =
        Attribute<OnKeyDown, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyDown' (receiver: obj, value: Func<Task>) =
        Attribute<OnKeyDown, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyDown' (receiver: obj, value: Func<KeyboardEventArgs, Task>) =
        Attribute<OnKeyDown, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))