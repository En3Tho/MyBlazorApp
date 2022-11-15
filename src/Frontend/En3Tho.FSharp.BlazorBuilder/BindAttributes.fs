namespace En3Tho.FSharp.BlazorBuilder

open System
open System.Globalization
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open System.Threading.Tasks
open En3Tho.FSharp.BlazorBuilder.Core
open En3Tho.FSharp.BlazorBuilder.Core.KnownAttributes
open Microsoft.AspNetCore.Components

type [<Struct; IsReadOnly>] BindAttribute<'attrName when 'attrName: struct and 'attrName :> IAttributeName>(initialValue: obj, callback: EventCallback<ChangeEventArgs>) =
    interface IAttribute with
        member _.RenderTo(builder) =
            builder.AddAttribute("value", initialValue)
            builder.AddAttribute(Unchecked.defaultof<'attrName>.Name, callback)
            builder.Builder.SetUpdatesAttributeName("value")

[<AbstractClass; Sealed; AutoOpen>]
type BindAttributes() =

    static member bindChange' (receiver: obj, existingValue, onChange: Action<'a>, [<Optional; DefaultParameterValue(null: CultureInfo)>] culture) =
        BindAttribute<OnChange>(BindConverter.FormatValue<'a>(existingValue, culture),
                      EventCallback.Factory.CreateBinder(receiver, onChange, existingValue, culture))

    static member bindChange' (receiver: obj, existingValue, onChange: Func<'a, Task>, [<Optional; DefaultParameterValue(null: CultureInfo)>] culture) =
        BindAttribute<OnChange>(BindConverter.FormatValue<'a>(existingValue, culture),
                      EventCallback.Factory.CreateBinder(receiver, onChange, existingValue, culture))

    static member bindChange' (receiver: obj, existingValue: int, onChange: Action<int>, [<Optional; DefaultParameterValue(null: CultureInfo)>] culture) =
        BindAttribute<OnChange>(BindConverter.FormatValue(existingValue, culture),
                      EventCallback.Factory.CreateBinder(receiver, onChange, existingValue, culture))

    static member bindChange' (receiver: obj, existingValue: int, onChange: Func<int, Task>, [<Optional; DefaultParameterValue(null: CultureInfo)>] culture) =
        BindAttribute<OnChange>(BindConverter.FormatValue(existingValue, culture),
                      EventCallback.Factory.CreateBinder(receiver, onChange, existingValue, culture))


    static member bindInput' (receiver: obj, existingValue, onChange: Action<'a>, [<Optional; DefaultParameterValue(null: CultureInfo)>] culture) =
        BindAttribute<OnInput>(BindConverter.FormatValue<'a>(existingValue, culture),
                      EventCallback.Factory.CreateBinder(receiver, onChange, existingValue, culture))

    static member bindInput' (receiver: obj, existingValue, onChange: Func<'a, Task>, [<Optional; DefaultParameterValue(null: CultureInfo)>] culture) =
        BindAttribute<OnInput>(BindConverter.FormatValue<'a>(existingValue, culture),
                      EventCallback.Factory.CreateBinder(receiver, onChange, existingValue, culture))

    static member bindInput' (receiver: obj, existingValue: int, onChange: Action<int>, [<Optional; DefaultParameterValue(null: CultureInfo)>] culture) =
        BindAttribute<OnInput>(BindConverter.FormatValue(existingValue, culture),
                      EventCallback.Factory.CreateBinder(receiver, onChange, existingValue, culture))

    static member bindInput' (receiver: obj, existingValue: int, onChange: Func<int, Task>, [<Optional; DefaultParameterValue(null: CultureInfo)>] culture) =
        BindAttribute<OnInput>(BindConverter.FormatValue(existingValue, culture),
                      EventCallback.Factory.CreateBinder(receiver, onChange, existingValue, culture))