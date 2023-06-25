// Auto-generated
namespace En3Tho.FSharp.BlazorBuilder
open System
open System.Threading.Tasks
open En3Tho.FSharp.BlazorBuilder.Core
open Microsoft.AspNetCore.Components
open Microsoft.AspNetCore.Components.Web

[<AbstractClass; Sealed; AutoOpen>]
type CallbackAttributes() =

    static member onAbort' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnAbort, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onAbort' (receiver: obj, value: Action<ProgressEventArgs>) =
        Attribute<KnownAttributes.OnAbort, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onAbort' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnAbort, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onAbort' (receiver: obj, value: EventCallback<ProgressEventArgs>) =
        Attribute<KnownAttributes.OnAbort, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onAbort' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnAbort, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onAbort' (receiver: obj, value: Func<ProgressEventArgs, Task>) =
        Attribute<KnownAttributes.OnAbort, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onLoad' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnLoad, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onLoad' (receiver: obj, value: Action<ProgressEventArgs>) =
        Attribute<KnownAttributes.OnLoad, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onLoad' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnLoad, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onLoad' (receiver: obj, value: EventCallback<ProgressEventArgs>) =
        Attribute<KnownAttributes.OnLoad, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onLoad' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnLoad, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onLoad' (receiver: obj, value: Func<ProgressEventArgs, Task>) =
        Attribute<KnownAttributes.OnLoad, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onLoadEnd' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnLoadEnd, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onLoadEnd' (receiver: obj, value: Action<ProgressEventArgs>) =
        Attribute<KnownAttributes.OnLoadEnd, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onLoadEnd' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnLoadEnd, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onLoadEnd' (receiver: obj, value: EventCallback<ProgressEventArgs>) =
        Attribute<KnownAttributes.OnLoadEnd, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onLoadEnd' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnLoadEnd, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onLoadEnd' (receiver: obj, value: Func<ProgressEventArgs, Task>) =
        Attribute<KnownAttributes.OnLoadEnd, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onLoadStart' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnLoadStart, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onLoadStart' (receiver: obj, value: Action<ProgressEventArgs>) =
        Attribute<KnownAttributes.OnLoadStart, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onLoadStart' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnLoadStart, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onLoadStart' (receiver: obj, value: EventCallback<ProgressEventArgs>) =
        Attribute<KnownAttributes.OnLoadStart, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onLoadStart' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnLoadStart, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onLoadStart' (receiver: obj, value: Func<ProgressEventArgs, Task>) =
        Attribute<KnownAttributes.OnLoadStart, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onProgress' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnProgress, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onProgress' (receiver: obj, value: Action<ProgressEventArgs>) =
        Attribute<KnownAttributes.OnProgress, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onProgress' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnProgress, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onProgress' (receiver: obj, value: EventCallback<ProgressEventArgs>) =
        Attribute<KnownAttributes.OnProgress, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onProgress' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnProgress, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onProgress' (receiver: obj, value: Func<ProgressEventArgs, Task>) =
        Attribute<KnownAttributes.OnProgress, _>(EventCallback.Factory.Create<ProgressEventArgs>(receiver, value))

    static member onBlur' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnBlur, _>(EventCallback.Factory.Create<FocusEventArgs>(receiver, value))

    static member onBlur' (receiver: obj, value: Action<FocusEventArgs>) =
        Attribute<KnownAttributes.OnBlur, _>(EventCallback.Factory.Create<FocusEventArgs>(receiver, value))

    static member onBlur' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnBlur, _>(EventCallback.Factory.Create<FocusEventArgs>(receiver, value))

    static member onBlur' (receiver: obj, value: EventCallback<FocusEventArgs>) =
        Attribute<KnownAttributes.OnBlur, _>(EventCallback.Factory.Create<FocusEventArgs>(receiver, value))

    static member onBlur' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnBlur, _>(EventCallback.Factory.Create<FocusEventArgs>(receiver, value))

    static member onBlur' (receiver: obj, value: Func<FocusEventArgs, Task>) =
        Attribute<KnownAttributes.OnBlur, _>(EventCallback.Factory.Create<FocusEventArgs>(receiver, value))

    static member onFocus' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnFocus, _>(EventCallback.Factory.Create<FocusEventArgs>(receiver, value))

    static member onFocus' (receiver: obj, value: Action<FocusEventArgs>) =
        Attribute<KnownAttributes.OnFocus, _>(EventCallback.Factory.Create<FocusEventArgs>(receiver, value))

    static member onFocus' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnFocus, _>(EventCallback.Factory.Create<FocusEventArgs>(receiver, value))

    static member onFocus' (receiver: obj, value: EventCallback<FocusEventArgs>) =
        Attribute<KnownAttributes.OnFocus, _>(EventCallback.Factory.Create<FocusEventArgs>(receiver, value))

    static member onFocus' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnFocus, _>(EventCallback.Factory.Create<FocusEventArgs>(receiver, value))

    static member onFocus' (receiver: obj, value: Func<FocusEventArgs, Task>) =
        Attribute<KnownAttributes.OnFocus, _>(EventCallback.Factory.Create<FocusEventArgs>(receiver, value))

    static member onFocusIn' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnFocusIn, _>(EventCallback.Factory.Create<FocusEventArgs>(receiver, value))

    static member onFocusIn' (receiver: obj, value: Action<FocusEventArgs>) =
        Attribute<KnownAttributes.OnFocusIn, _>(EventCallback.Factory.Create<FocusEventArgs>(receiver, value))

    static member onFocusIn' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnFocusIn, _>(EventCallback.Factory.Create<FocusEventArgs>(receiver, value))

    static member onFocusIn' (receiver: obj, value: EventCallback<FocusEventArgs>) =
        Attribute<KnownAttributes.OnFocusIn, _>(EventCallback.Factory.Create<FocusEventArgs>(receiver, value))

    static member onFocusIn' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnFocusIn, _>(EventCallback.Factory.Create<FocusEventArgs>(receiver, value))

    static member onFocusIn' (receiver: obj, value: Func<FocusEventArgs, Task>) =
        Attribute<KnownAttributes.OnFocusIn, _>(EventCallback.Factory.Create<FocusEventArgs>(receiver, value))

    static member onFocusOut' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnFocusOut, _>(EventCallback.Factory.Create<FocusEventArgs>(receiver, value))

    static member onFocusOut' (receiver: obj, value: Action<FocusEventArgs>) =
        Attribute<KnownAttributes.OnFocusOut, _>(EventCallback.Factory.Create<FocusEventArgs>(receiver, value))

    static member onFocusOut' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnFocusOut, _>(EventCallback.Factory.Create<FocusEventArgs>(receiver, value))

    static member onFocusOut' (receiver: obj, value: EventCallback<FocusEventArgs>) =
        Attribute<KnownAttributes.OnFocusOut, _>(EventCallback.Factory.Create<FocusEventArgs>(receiver, value))

    static member onFocusOut' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnFocusOut, _>(EventCallback.Factory.Create<FocusEventArgs>(receiver, value))

    static member onFocusOut' (receiver: obj, value: Func<FocusEventArgs, Task>) =
        Attribute<KnownAttributes.OnFocusOut, _>(EventCallback.Factory.Create<FocusEventArgs>(receiver, value))

    static member onInput' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnInput, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onInput' (receiver: obj, value: Action<ChangeEventArgs>) =
        Attribute<KnownAttributes.OnInput, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onInput' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnInput, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onInput' (receiver: obj, value: EventCallback<ChangeEventArgs>) =
        Attribute<KnownAttributes.OnInput, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onInput' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnInput, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onInput' (receiver: obj, value: Func<ChangeEventArgs, Task>) =
        Attribute<KnownAttributes.OnInput, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onChange' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnChange, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onChange' (receiver: obj, value: Action<ChangeEventArgs>) =
        Attribute<KnownAttributes.OnChange, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onChange' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnChange, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onChange' (receiver: obj, value: EventCallback<ChangeEventArgs>) =
        Attribute<KnownAttributes.OnChange, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onChange' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnChange, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onChange' (receiver: obj, value: Func<ChangeEventArgs, Task>) =
        Attribute<KnownAttributes.OnChange, _>(EventCallback.Factory.Create<ChangeEventArgs>(receiver, value))

    static member onClick' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnClick, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onClick' (receiver: obj, value: Action<MouseEventArgs>) =
        Attribute<KnownAttributes.OnClick, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onClick' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnClick, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onClick' (receiver: obj, value: EventCallback<MouseEventArgs>) =
        Attribute<KnownAttributes.OnClick, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onClick' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnClick, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onClick' (receiver: obj, value: Func<MouseEventArgs, Task>) =
        Attribute<KnownAttributes.OnClick, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onContextMenu' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnContextMenu, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onContextMenu' (receiver: obj, value: Action<MouseEventArgs>) =
        Attribute<KnownAttributes.OnContextMenu, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onContextMenu' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnContextMenu, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onContextMenu' (receiver: obj, value: EventCallback<MouseEventArgs>) =
        Attribute<KnownAttributes.OnContextMenu, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onContextMenu' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnContextMenu, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onContextMenu' (receiver: obj, value: Func<MouseEventArgs, Task>) =
        Attribute<KnownAttributes.OnContextMenu, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseDown' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnMouseDown, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseDown' (receiver: obj, value: Action<MouseEventArgs>) =
        Attribute<KnownAttributes.OnMouseDown, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseDown' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnMouseDown, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseDown' (receiver: obj, value: EventCallback<MouseEventArgs>) =
        Attribute<KnownAttributes.OnMouseDown, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseDown' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnMouseDown, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseDown' (receiver: obj, value: Func<MouseEventArgs, Task>) =
        Attribute<KnownAttributes.OnMouseDown, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseEnter' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnMouseEnter, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseEnter' (receiver: obj, value: Action<MouseEventArgs>) =
        Attribute<KnownAttributes.OnMouseEnter, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseEnter' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnMouseEnter, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseEnter' (receiver: obj, value: EventCallback<MouseEventArgs>) =
        Attribute<KnownAttributes.OnMouseEnter, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseEnter' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnMouseEnter, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseEnter' (receiver: obj, value: Func<MouseEventArgs, Task>) =
        Attribute<KnownAttributes.OnMouseEnter, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseLeave' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnMouseLeave, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseLeave' (receiver: obj, value: Action<MouseEventArgs>) =
        Attribute<KnownAttributes.OnMouseLeave, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseLeave' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnMouseLeave, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseLeave' (receiver: obj, value: EventCallback<MouseEventArgs>) =
        Attribute<KnownAttributes.OnMouseLeave, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseLeave' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnMouseLeave, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseLeave' (receiver: obj, value: Func<MouseEventArgs, Task>) =
        Attribute<KnownAttributes.OnMouseLeave, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseMove' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnMouseMove, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseMove' (receiver: obj, value: Action<MouseEventArgs>) =
        Attribute<KnownAttributes.OnMouseMove, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseMove' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnMouseMove, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseMove' (receiver: obj, value: EventCallback<MouseEventArgs>) =
        Attribute<KnownAttributes.OnMouseMove, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseMove' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnMouseMove, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseMove' (receiver: obj, value: Func<MouseEventArgs, Task>) =
        Attribute<KnownAttributes.OnMouseMove, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseOut' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnMouseOut, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseOut' (receiver: obj, value: Action<MouseEventArgs>) =
        Attribute<KnownAttributes.OnMouseOut, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseOut' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnMouseOut, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseOut' (receiver: obj, value: EventCallback<MouseEventArgs>) =
        Attribute<KnownAttributes.OnMouseOut, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseOut' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnMouseOut, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseOut' (receiver: obj, value: Func<MouseEventArgs, Task>) =
        Attribute<KnownAttributes.OnMouseOut, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseOver' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnMouseOver, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseOver' (receiver: obj, value: Action<MouseEventArgs>) =
        Attribute<KnownAttributes.OnMouseOver, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseOver' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnMouseOver, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseOver' (receiver: obj, value: EventCallback<MouseEventArgs>) =
        Attribute<KnownAttributes.OnMouseOver, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseOver' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnMouseOver, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseOver' (receiver: obj, value: Func<MouseEventArgs, Task>) =
        Attribute<KnownAttributes.OnMouseOver, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseUp' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnMouseUp, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseUp' (receiver: obj, value: Action<MouseEventArgs>) =
        Attribute<KnownAttributes.OnMouseUp, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseUp' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnMouseUp, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseUp' (receiver: obj, value: EventCallback<MouseEventArgs>) =
        Attribute<KnownAttributes.OnMouseUp, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseUp' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnMouseUp, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onMouseUp' (receiver: obj, value: Func<MouseEventArgs, Task>) =
        Attribute<KnownAttributes.OnMouseUp, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onDblClick' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnDblClick, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onDblClick' (receiver: obj, value: Action<MouseEventArgs>) =
        Attribute<KnownAttributes.OnDblClick, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onDblClick' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnDblClick, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onDblClick' (receiver: obj, value: EventCallback<MouseEventArgs>) =
        Attribute<KnownAttributes.OnDblClick, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onDblClick' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnDblClick, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onDblClick' (receiver: obj, value: Func<MouseEventArgs, Task>) =
        Attribute<KnownAttributes.OnDblClick, _>(EventCallback.Factory.Create<MouseEventArgs>(receiver, value))

    static member onCopy' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnCopy, _>(EventCallback.Factory.Create<ClipboardEventArgs>(receiver, value))

    static member onCopy' (receiver: obj, value: Action<ClipboardEventArgs>) =
        Attribute<KnownAttributes.OnCopy, _>(EventCallback.Factory.Create<ClipboardEventArgs>(receiver, value))

    static member onCopy' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnCopy, _>(EventCallback.Factory.Create<ClipboardEventArgs>(receiver, value))

    static member onCopy' (receiver: obj, value: EventCallback<ClipboardEventArgs>) =
        Attribute<KnownAttributes.OnCopy, _>(EventCallback.Factory.Create<ClipboardEventArgs>(receiver, value))

    static member onCopy' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnCopy, _>(EventCallback.Factory.Create<ClipboardEventArgs>(receiver, value))

    static member onCopy' (receiver: obj, value: Func<ClipboardEventArgs, Task>) =
        Attribute<KnownAttributes.OnCopy, _>(EventCallback.Factory.Create<ClipboardEventArgs>(receiver, value))

    static member onCut' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnCut, _>(EventCallback.Factory.Create<ClipboardEventArgs>(receiver, value))

    static member onCut' (receiver: obj, value: Action<ClipboardEventArgs>) =
        Attribute<KnownAttributes.OnCut, _>(EventCallback.Factory.Create<ClipboardEventArgs>(receiver, value))

    static member onCut' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnCut, _>(EventCallback.Factory.Create<ClipboardEventArgs>(receiver, value))

    static member onCut' (receiver: obj, value: EventCallback<ClipboardEventArgs>) =
        Attribute<KnownAttributes.OnCut, _>(EventCallback.Factory.Create<ClipboardEventArgs>(receiver, value))

    static member onCut' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnCut, _>(EventCallback.Factory.Create<ClipboardEventArgs>(receiver, value))

    static member onCut' (receiver: obj, value: Func<ClipboardEventArgs, Task>) =
        Attribute<KnownAttributes.OnCut, _>(EventCallback.Factory.Create<ClipboardEventArgs>(receiver, value))

    static member onPaste' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnPaste, _>(EventCallback.Factory.Create<ClipboardEventArgs>(receiver, value))

    static member onPaste' (receiver: obj, value: Action<ClipboardEventArgs>) =
        Attribute<KnownAttributes.OnPaste, _>(EventCallback.Factory.Create<ClipboardEventArgs>(receiver, value))

    static member onPaste' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnPaste, _>(EventCallback.Factory.Create<ClipboardEventArgs>(receiver, value))

    static member onPaste' (receiver: obj, value: EventCallback<ClipboardEventArgs>) =
        Attribute<KnownAttributes.OnPaste, _>(EventCallback.Factory.Create<ClipboardEventArgs>(receiver, value))

    static member onPaste' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnPaste, _>(EventCallback.Factory.Create<ClipboardEventArgs>(receiver, value))

    static member onPaste' (receiver: obj, value: Func<ClipboardEventArgs, Task>) =
        Attribute<KnownAttributes.OnPaste, _>(EventCallback.Factory.Create<ClipboardEventArgs>(receiver, value))

    static member onDrag' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnDrag, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDrag' (receiver: obj, value: Action<DragEventArgs>) =
        Attribute<KnownAttributes.OnDrag, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDrag' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnDrag, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDrag' (receiver: obj, value: EventCallback<DragEventArgs>) =
        Attribute<KnownAttributes.OnDrag, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDrag' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnDrag, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDrag' (receiver: obj, value: Func<DragEventArgs, Task>) =
        Attribute<KnownAttributes.OnDrag, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragEnd' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnDragEnd, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragEnd' (receiver: obj, value: Action<DragEventArgs>) =
        Attribute<KnownAttributes.OnDragEnd, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragEnd' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnDragEnd, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragEnd' (receiver: obj, value: EventCallback<DragEventArgs>) =
        Attribute<KnownAttributes.OnDragEnd, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragEnd' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnDragEnd, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragEnd' (receiver: obj, value: Func<DragEventArgs, Task>) =
        Attribute<KnownAttributes.OnDragEnd, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragEnter' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnDragEnter, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragEnter' (receiver: obj, value: Action<DragEventArgs>) =
        Attribute<KnownAttributes.OnDragEnter, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragEnter' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnDragEnter, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragEnter' (receiver: obj, value: EventCallback<DragEventArgs>) =
        Attribute<KnownAttributes.OnDragEnter, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragEnter' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnDragEnter, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragEnter' (receiver: obj, value: Func<DragEventArgs, Task>) =
        Attribute<KnownAttributes.OnDragEnter, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragExit' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnDragExit, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragExit' (receiver: obj, value: Action<DragEventArgs>) =
        Attribute<KnownAttributes.OnDragExit, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragExit' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnDragExit, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragExit' (receiver: obj, value: EventCallback<DragEventArgs>) =
        Attribute<KnownAttributes.OnDragExit, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragExit' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnDragExit, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragExit' (receiver: obj, value: Func<DragEventArgs, Task>) =
        Attribute<KnownAttributes.OnDragExit, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragLeave' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnDragLeave, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragLeave' (receiver: obj, value: Action<DragEventArgs>) =
        Attribute<KnownAttributes.OnDragLeave, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragLeave' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnDragLeave, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragLeave' (receiver: obj, value: EventCallback<DragEventArgs>) =
        Attribute<KnownAttributes.OnDragLeave, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragLeave' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnDragLeave, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragLeave' (receiver: obj, value: Func<DragEventArgs, Task>) =
        Attribute<KnownAttributes.OnDragLeave, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragOver' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnDragOver, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragOver' (receiver: obj, value: Action<DragEventArgs>) =
        Attribute<KnownAttributes.OnDragOver, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragOver' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnDragOver, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragOver' (receiver: obj, value: EventCallback<DragEventArgs>) =
        Attribute<KnownAttributes.OnDragOver, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragOver' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnDragOver, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragOver' (receiver: obj, value: Func<DragEventArgs, Task>) =
        Attribute<KnownAttributes.OnDragOver, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragStart' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnDragStart, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragStart' (receiver: obj, value: Action<DragEventArgs>) =
        Attribute<KnownAttributes.OnDragStart, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragStart' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnDragStart, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragStart' (receiver: obj, value: EventCallback<DragEventArgs>) =
        Attribute<KnownAttributes.OnDragStart, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragStart' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnDragStart, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDragStart' (receiver: obj, value: Func<DragEventArgs, Task>) =
        Attribute<KnownAttributes.OnDragStart, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDrop' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnDrop, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDrop' (receiver: obj, value: Action<DragEventArgs>) =
        Attribute<KnownAttributes.OnDrop, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDrop' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnDrop, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDrop' (receiver: obj, value: EventCallback<DragEventArgs>) =
        Attribute<KnownAttributes.OnDrop, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDrop' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnDrop, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onDrop' (receiver: obj, value: Func<DragEventArgs, Task>) =
        Attribute<KnownAttributes.OnDrop, _>(EventCallback.Factory.Create<DragEventArgs>(receiver, value))

    static member onGotPointerCapture' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnGotPointerCapture, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onGotPointerCapture' (receiver: obj, value: Action<PointerEventArgs>) =
        Attribute<KnownAttributes.OnGotPointerCapture, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onGotPointerCapture' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnGotPointerCapture, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onGotPointerCapture' (receiver: obj, value: EventCallback<PointerEventArgs>) =
        Attribute<KnownAttributes.OnGotPointerCapture, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onGotPointerCapture' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnGotPointerCapture, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onGotPointerCapture' (receiver: obj, value: Func<PointerEventArgs, Task>) =
        Attribute<KnownAttributes.OnGotPointerCapture, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onLostPointerCapture' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnLostPointerCapture, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onLostPointerCapture' (receiver: obj, value: Action<PointerEventArgs>) =
        Attribute<KnownAttributes.OnLostPointerCapture, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onLostPointerCapture' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnLostPointerCapture, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onLostPointerCapture' (receiver: obj, value: EventCallback<PointerEventArgs>) =
        Attribute<KnownAttributes.OnLostPointerCapture, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onLostPointerCapture' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnLostPointerCapture, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onLostPointerCapture' (receiver: obj, value: Func<PointerEventArgs, Task>) =
        Attribute<KnownAttributes.OnLostPointerCapture, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerCancel' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnPointerCancel, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerCancel' (receiver: obj, value: Action<PointerEventArgs>) =
        Attribute<KnownAttributes.OnPointerCancel, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerCancel' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnPointerCancel, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerCancel' (receiver: obj, value: EventCallback<PointerEventArgs>) =
        Attribute<KnownAttributes.OnPointerCancel, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerCancel' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnPointerCancel, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerCancel' (receiver: obj, value: Func<PointerEventArgs, Task>) =
        Attribute<KnownAttributes.OnPointerCancel, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerDown' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnPointerDown, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerDown' (receiver: obj, value: Action<PointerEventArgs>) =
        Attribute<KnownAttributes.OnPointerDown, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerDown' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnPointerDown, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerDown' (receiver: obj, value: EventCallback<PointerEventArgs>) =
        Attribute<KnownAttributes.OnPointerDown, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerDown' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnPointerDown, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerDown' (receiver: obj, value: Func<PointerEventArgs, Task>) =
        Attribute<KnownAttributes.OnPointerDown, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerEnter' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnPointerEnter, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerEnter' (receiver: obj, value: Action<PointerEventArgs>) =
        Attribute<KnownAttributes.OnPointerEnter, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerEnter' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnPointerEnter, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerEnter' (receiver: obj, value: EventCallback<PointerEventArgs>) =
        Attribute<KnownAttributes.OnPointerEnter, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerEnter' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnPointerEnter, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerEnter' (receiver: obj, value: Func<PointerEventArgs, Task>) =
        Attribute<KnownAttributes.OnPointerEnter, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerLeave' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnPointerLeave, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerLeave' (receiver: obj, value: Action<PointerEventArgs>) =
        Attribute<KnownAttributes.OnPointerLeave, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerLeave' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnPointerLeave, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerLeave' (receiver: obj, value: EventCallback<PointerEventArgs>) =
        Attribute<KnownAttributes.OnPointerLeave, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerLeave' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnPointerLeave, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerLeave' (receiver: obj, value: Func<PointerEventArgs, Task>) =
        Attribute<KnownAttributes.OnPointerLeave, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerMove' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnPointerMove, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerMove' (receiver: obj, value: Action<PointerEventArgs>) =
        Attribute<KnownAttributes.OnPointerMove, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerMove' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnPointerMove, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerMove' (receiver: obj, value: EventCallback<PointerEventArgs>) =
        Attribute<KnownAttributes.OnPointerMove, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerMove' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnPointerMove, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerMove' (receiver: obj, value: Func<PointerEventArgs, Task>) =
        Attribute<KnownAttributes.OnPointerMove, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerOut' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnPointerOut, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerOut' (receiver: obj, value: Action<PointerEventArgs>) =
        Attribute<KnownAttributes.OnPointerOut, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerOut' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnPointerOut, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerOut' (receiver: obj, value: EventCallback<PointerEventArgs>) =
        Attribute<KnownAttributes.OnPointerOut, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerOut' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnPointerOut, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerOut' (receiver: obj, value: Func<PointerEventArgs, Task>) =
        Attribute<KnownAttributes.OnPointerOut, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerOver' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnPointerOver, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerOver' (receiver: obj, value: Action<PointerEventArgs>) =
        Attribute<KnownAttributes.OnPointerOver, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerOver' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnPointerOver, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerOver' (receiver: obj, value: EventCallback<PointerEventArgs>) =
        Attribute<KnownAttributes.OnPointerOver, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerOver' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnPointerOver, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerOver' (receiver: obj, value: Func<PointerEventArgs, Task>) =
        Attribute<KnownAttributes.OnPointerOver, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerUp' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnPointerUp, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerUp' (receiver: obj, value: Action<PointerEventArgs>) =
        Attribute<KnownAttributes.OnPointerUp, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerUp' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnPointerUp, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerUp' (receiver: obj, value: EventCallback<PointerEventArgs>) =
        Attribute<KnownAttributes.OnPointerUp, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerUp' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnPointerUp, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onPointerUp' (receiver: obj, value: Func<PointerEventArgs, Task>) =
        Attribute<KnownAttributes.OnPointerUp, _>(EventCallback.Factory.Create<PointerEventArgs>(receiver, value))

    static member onKeyDown' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnKeyDown, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyDown' (receiver: obj, value: Action<KeyboardEventArgs>) =
        Attribute<KnownAttributes.OnKeyDown, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyDown' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnKeyDown, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyDown' (receiver: obj, value: EventCallback<KeyboardEventArgs>) =
        Attribute<KnownAttributes.OnKeyDown, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyDown' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnKeyDown, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyDown' (receiver: obj, value: Func<KeyboardEventArgs, Task>) =
        Attribute<KnownAttributes.OnKeyDown, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyPress' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnKeyPress, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyPress' (receiver: obj, value: Action<KeyboardEventArgs>) =
        Attribute<KnownAttributes.OnKeyPress, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyPress' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnKeyPress, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyPress' (receiver: obj, value: EventCallback<KeyboardEventArgs>) =
        Attribute<KnownAttributes.OnKeyPress, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyPress' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnKeyPress, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyPress' (receiver: obj, value: Func<KeyboardEventArgs, Task>) =
        Attribute<KnownAttributes.OnKeyPress, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyUp' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnKeyUp, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyUp' (receiver: obj, value: Action<KeyboardEventArgs>) =
        Attribute<KnownAttributes.OnKeyUp, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyUp' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnKeyUp, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyUp' (receiver: obj, value: EventCallback<KeyboardEventArgs>) =
        Attribute<KnownAttributes.OnKeyUp, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyUp' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnKeyUp, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onKeyUp' (receiver: obj, value: Func<KeyboardEventArgs, Task>) =
        Attribute<KnownAttributes.OnKeyUp, _>(EventCallback.Factory.Create<KeyboardEventArgs>(receiver, value))

    static member onTouchCancel' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnTouchCancel, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchCancel' (receiver: obj, value: Action<TouchEventArgs>) =
        Attribute<KnownAttributes.OnTouchCancel, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchCancel' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnTouchCancel, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchCancel' (receiver: obj, value: EventCallback<TouchEventArgs>) =
        Attribute<KnownAttributes.OnTouchCancel, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchCancel' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnTouchCancel, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchCancel' (receiver: obj, value: Func<TouchEventArgs, Task>) =
        Attribute<KnownAttributes.OnTouchCancel, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchEnd' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnTouchEnd, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchEnd' (receiver: obj, value: Action<TouchEventArgs>) =
        Attribute<KnownAttributes.OnTouchEnd, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchEnd' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnTouchEnd, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchEnd' (receiver: obj, value: EventCallback<TouchEventArgs>) =
        Attribute<KnownAttributes.OnTouchEnd, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchEnd' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnTouchEnd, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchEnd' (receiver: obj, value: Func<TouchEventArgs, Task>) =
        Attribute<KnownAttributes.OnTouchEnd, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchEnter' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnTouchEnter, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchEnter' (receiver: obj, value: Action<TouchEventArgs>) =
        Attribute<KnownAttributes.OnTouchEnter, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchEnter' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnTouchEnter, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchEnter' (receiver: obj, value: EventCallback<TouchEventArgs>) =
        Attribute<KnownAttributes.OnTouchEnter, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchEnter' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnTouchEnter, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchEnter' (receiver: obj, value: Func<TouchEventArgs, Task>) =
        Attribute<KnownAttributes.OnTouchEnter, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchLeave' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnTouchLeave, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchLeave' (receiver: obj, value: Action<TouchEventArgs>) =
        Attribute<KnownAttributes.OnTouchLeave, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchLeave' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnTouchLeave, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchLeave' (receiver: obj, value: EventCallback<TouchEventArgs>) =
        Attribute<KnownAttributes.OnTouchLeave, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchLeave' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnTouchLeave, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchLeave' (receiver: obj, value: Func<TouchEventArgs, Task>) =
        Attribute<KnownAttributes.OnTouchLeave, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchMove' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnTouchMove, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchMove' (receiver: obj, value: Action<TouchEventArgs>) =
        Attribute<KnownAttributes.OnTouchMove, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchMove' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnTouchMove, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchMove' (receiver: obj, value: EventCallback<TouchEventArgs>) =
        Attribute<KnownAttributes.OnTouchMove, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchMove' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnTouchMove, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchMove' (receiver: obj, value: Func<TouchEventArgs, Task>) =
        Attribute<KnownAttributes.OnTouchMove, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchStart' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnTouchStart, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchStart' (receiver: obj, value: Action<TouchEventArgs>) =
        Attribute<KnownAttributes.OnTouchStart, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchStart' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnTouchStart, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchStart' (receiver: obj, value: EventCallback<TouchEventArgs>) =
        Attribute<KnownAttributes.OnTouchStart, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchStart' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnTouchStart, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onTouchStart' (receiver: obj, value: Func<TouchEventArgs, Task>) =
        Attribute<KnownAttributes.OnTouchStart, _>(EventCallback.Factory.Create<TouchEventArgs>(receiver, value))

    static member onError' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnError, _>(EventCallback.Factory.Create<ErrorEventArgs>(receiver, value))

    static member onError' (receiver: obj, value: Action<ErrorEventArgs>) =
        Attribute<KnownAttributes.OnError, _>(EventCallback.Factory.Create<ErrorEventArgs>(receiver, value))

    static member onError' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnError, _>(EventCallback.Factory.Create<ErrorEventArgs>(receiver, value))

    static member onError' (receiver: obj, value: EventCallback<ErrorEventArgs>) =
        Attribute<KnownAttributes.OnError, _>(EventCallback.Factory.Create<ErrorEventArgs>(receiver, value))

    static member onError' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnError, _>(EventCallback.Factory.Create<ErrorEventArgs>(receiver, value))

    static member onError' (receiver: obj, value: Func<ErrorEventArgs, Task>) =
        Attribute<KnownAttributes.OnError, _>(EventCallback.Factory.Create<ErrorEventArgs>(receiver, value))

    static member onLoadedMetadata' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnLoadedMetadata, _>(EventCallback.Factory.Create<WheelEventArgs>(receiver, value))

    static member onLoadedMetadata' (receiver: obj, value: Action<WheelEventArgs>) =
        Attribute<KnownAttributes.OnLoadedMetadata, _>(EventCallback.Factory.Create<WheelEventArgs>(receiver, value))

    static member onLoadedMetadata' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnLoadedMetadata, _>(EventCallback.Factory.Create<WheelEventArgs>(receiver, value))

    static member onLoadedMetadata' (receiver: obj, value: EventCallback<WheelEventArgs>) =
        Attribute<KnownAttributes.OnLoadedMetadata, _>(EventCallback.Factory.Create<WheelEventArgs>(receiver, value))

    static member onLoadedMetadata' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnLoadedMetadata, _>(EventCallback.Factory.Create<WheelEventArgs>(receiver, value))

    static member onLoadedMetadata' (receiver: obj, value: Func<WheelEventArgs, Task>) =
        Attribute<KnownAttributes.OnLoadedMetadata, _>(EventCallback.Factory.Create<WheelEventArgs>(receiver, value))

    static member onMouseWheel' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnMouseWheel, _>(EventCallback.Factory.Create<WheelEventArgs>(receiver, value))

    static member onMouseWheel' (receiver: obj, value: Action<WheelEventArgs>) =
        Attribute<KnownAttributes.OnMouseWheel, _>(EventCallback.Factory.Create<WheelEventArgs>(receiver, value))

    static member onMouseWheel' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnMouseWheel, _>(EventCallback.Factory.Create<WheelEventArgs>(receiver, value))

    static member onMouseWheel' (receiver: obj, value: EventCallback<WheelEventArgs>) =
        Attribute<KnownAttributes.OnMouseWheel, _>(EventCallback.Factory.Create<WheelEventArgs>(receiver, value))

    static member onMouseWheel' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnMouseWheel, _>(EventCallback.Factory.Create<WheelEventArgs>(receiver, value))

    static member onMouseWheel' (receiver: obj, value: Func<WheelEventArgs, Task>) =
        Attribute<KnownAttributes.OnMouseWheel, _>(EventCallback.Factory.Create<WheelEventArgs>(receiver, value))

    static member onWheel' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnWheel, _>(EventCallback.Factory.Create<WheelEventArgs>(receiver, value))

    static member onWheel' (receiver: obj, value: Action<WheelEventArgs>) =
        Attribute<KnownAttributes.OnWheel, _>(EventCallback.Factory.Create<WheelEventArgs>(receiver, value))

    static member onWheel' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnWheel, _>(EventCallback.Factory.Create<WheelEventArgs>(receiver, value))

    static member onWheel' (receiver: obj, value: EventCallback<WheelEventArgs>) =
        Attribute<KnownAttributes.OnWheel, _>(EventCallback.Factory.Create<WheelEventArgs>(receiver, value))

    static member onWheel' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnWheel, _>(EventCallback.Factory.Create<WheelEventArgs>(receiver, value))

    static member onWheel' (receiver: obj, value: Func<WheelEventArgs, Task>) =
        Attribute<KnownAttributes.OnWheel, _>(EventCallback.Factory.Create<WheelEventArgs>(receiver, value))

    static member onActivate' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnActivate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onActivate' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnActivate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onActivate' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnActivate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onActivate' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnActivate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onActivate' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnActivate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onActivate' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnActivate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforeActivate' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnBeforeActivate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforeActivate' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnBeforeActivate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforeActivate' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnBeforeActivate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforeActivate' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnBeforeActivate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforeActivate' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnBeforeActivate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforeActivate' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnBeforeActivate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforeCopy' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnBeforeCopy, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforeCopy' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnBeforeCopy, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforeCopy' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnBeforeCopy, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforeCopy' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnBeforeCopy, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforeCopy' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnBeforeCopy, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforeCopy' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnBeforeCopy, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforeCut' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnBeforeCut, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforeCut' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnBeforeCut, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforeCut' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnBeforeCut, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforeCut' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnBeforeCut, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforeCut' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnBeforeCut, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforeCut' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnBeforeCut, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforeDeactivate' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnBeforeDeactivate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforeDeactivate' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnBeforeDeactivate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforeDeactivate' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnBeforeDeactivate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforeDeactivate' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnBeforeDeactivate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforeDeactivate' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnBeforeDeactivate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforeDeactivate' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnBeforeDeactivate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforePaste' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnBeforePaste, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforePaste' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnBeforePaste, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforePaste' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnBeforePaste, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforePaste' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnBeforePaste, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforePaste' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnBeforePaste, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onBeforePaste' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnBeforePaste, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onCanPlay' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnCanPlay, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onCanPlay' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnCanPlay, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onCanPlay' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnCanPlay, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onCanPlay' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnCanPlay, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onCanPlay' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnCanPlay, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onCanPlay' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnCanPlay, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onCanPlayThrough' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnCanPlayThrough, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onCanPlayThrough' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnCanPlayThrough, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onCanPlayThrough' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnCanPlayThrough, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onCanPlayThrough' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnCanPlayThrough, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onCanPlayThrough' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnCanPlayThrough, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onCanPlayThrough' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnCanPlayThrough, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onCueChange' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnCueChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onCueChange' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnCueChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onCueChange' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnCueChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onCueChange' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnCueChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onCueChange' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnCueChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onCueChange' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnCueChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onDeactivate' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnDeactivate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onDeactivate' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnDeactivate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onDeactivate' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnDeactivate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onDeactivate' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnDeactivate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onDeactivate' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnDeactivate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onDeactivate' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnDeactivate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onDurationChange' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnDurationChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onDurationChange' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnDurationChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onDurationChange' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnDurationChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onDurationChange' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnDurationChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onDurationChange' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnDurationChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onDurationChange' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnDurationChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onEmptied' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnEmptied, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onEmptied' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnEmptied, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onEmptied' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnEmptied, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onEmptied' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnEmptied, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onEmptied' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnEmptied, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onEmptied' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnEmptied, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onEnded' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnEnded, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onEnded' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnEnded, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onEnded' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnEnded, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onEnded' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnEnded, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onEnded' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnEnded, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onEnded' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnEnded, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onFullscreenChange' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnFullscreenChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onFullscreenChange' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnFullscreenChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onFullscreenChange' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnFullscreenChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onFullscreenChange' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnFullscreenChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onFullscreenChange' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnFullscreenChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onFullscreenChange' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnFullscreenChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onFullscreenError' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnFullscreenError, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onFullscreenError' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnFullscreenError, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onFullscreenError' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnFullscreenError, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onFullscreenError' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnFullscreenError, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onFullscreenError' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnFullscreenError, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onFullscreenError' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnFullscreenError, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onInvalid' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnInvalid, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onInvalid' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnInvalid, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onInvalid' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnInvalid, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onInvalid' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnInvalid, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onInvalid' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnInvalid, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onInvalid' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnInvalid, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onLoadedData' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnLoadedData, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onLoadedData' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnLoadedData, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onLoadedData' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnLoadedData, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onLoadedData' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnLoadedData, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onLoadedData' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnLoadedData, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onLoadedData' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnLoadedData, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPause' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnPause, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPause' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnPause, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPause' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnPause, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPause' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnPause, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPause' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnPause, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPause' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnPause, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPlay' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnPlay, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPlay' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnPlay, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPlay' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnPlay, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPlay' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnPlay, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPlay' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnPlay, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPlay' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnPlay, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPlaying' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnPlaying, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPlaying' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnPlaying, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPlaying' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnPlaying, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPlaying' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnPlaying, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPlaying' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnPlaying, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPlaying' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnPlaying, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPointerLockChange' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnPointerLockChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPointerLockChange' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnPointerLockChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPointerLockChange' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnPointerLockChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPointerLockChange' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnPointerLockChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPointerLockChange' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnPointerLockChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPointerLockChange' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnPointerLockChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPointerLockError' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnPointerLockError, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPointerLockError' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnPointerLockError, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPointerLockError' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnPointerLockError, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPointerLockError' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnPointerLockError, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPointerLockError' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnPointerLockError, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onPointerLockError' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnPointerLockError, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onRateChange' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnRateChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onRateChange' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnRateChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onRateChange' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnRateChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onRateChange' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnRateChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onRateChange' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnRateChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onRateChange' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnRateChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onReadyStateChange' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnReadyStateChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onReadyStateChange' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnReadyStateChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onReadyStateChange' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnReadyStateChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onReadyStateChange' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnReadyStateChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onReadyStateChange' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnReadyStateChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onReadyStateChange' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnReadyStateChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onReset' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnReset, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onReset' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnReset, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onReset' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnReset, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onReset' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnReset, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onReset' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnReset, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onReset' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnReset, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onScroll' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnScroll, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onScroll' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnScroll, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onScroll' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnScroll, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onScroll' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnScroll, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onScroll' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnScroll, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onScroll' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnScroll, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSeeked' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnSeeked, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSeeked' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnSeeked, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSeeked' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnSeeked, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSeeked' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnSeeked, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSeeked' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnSeeked, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSeeked' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnSeeked, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSeeking' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnSeeking, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSeeking' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnSeeking, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSeeking' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnSeeking, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSeeking' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnSeeking, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSeeking' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnSeeking, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSeeking' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnSeeking, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSelect' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnSelect, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSelect' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnSelect, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSelect' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnSelect, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSelect' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnSelect, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSelect' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnSelect, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSelect' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnSelect, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSelectionChange' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnSelectionChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSelectionChange' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnSelectionChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSelectionChange' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnSelectionChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSelectionChange' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnSelectionChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSelectionChange' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnSelectionChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSelectionChange' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnSelectionChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSelectStart' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnSelectStart, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSelectStart' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnSelectStart, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSelectStart' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnSelectStart, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSelectStart' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnSelectStart, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSelectStart' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnSelectStart, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSelectStart' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnSelectStart, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onStalled' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnStalled, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onStalled' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnStalled, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onStalled' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnStalled, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onStalled' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnStalled, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onStalled' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnStalled, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onStalled' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnStalled, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onStop' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnStop, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onStop' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnStop, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onStop' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnStop, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onStop' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnStop, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onStop' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnStop, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onStop' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnStop, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSubmit' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnSubmit, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSubmit' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnSubmit, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSubmit' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnSubmit, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSubmit' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnSubmit, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSubmit' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnSubmit, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSubmit' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnSubmit, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSuspend' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnSuspend, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSuspend' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnSuspend, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSuspend' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnSuspend, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSuspend' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnSuspend, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSuspend' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnSuspend, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onSuspend' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnSuspend, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onTimeout' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnTimeout, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onTimeout' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnTimeout, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onTimeout' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnTimeout, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onTimeout' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnTimeout, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onTimeout' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnTimeout, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onTimeout' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnTimeout, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onTimeUpdate' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnTimeUpdate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onTimeUpdate' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnTimeUpdate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onTimeUpdate' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnTimeUpdate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onTimeUpdate' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnTimeUpdate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onTimeUpdate' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnTimeUpdate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onTimeUpdate' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnTimeUpdate, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onToggle' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnToggle, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onToggle' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnToggle, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onToggle' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnToggle, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onToggle' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnToggle, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onToggle' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnToggle, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onToggle' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnToggle, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onVolumeChange' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnVolumeChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onVolumeChange' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnVolumeChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onVolumeChange' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnVolumeChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onVolumeChange' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnVolumeChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onVolumeChange' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnVolumeChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onVolumeChange' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnVolumeChange, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onWaiting' (receiver: obj, value: Action) =
        Attribute<KnownAttributes.OnWaiting, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onWaiting' (receiver: obj, value: Action<EventArgs>) =
        Attribute<KnownAttributes.OnWaiting, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onWaiting' (receiver: obj, value: EventCallback) =
        Attribute<KnownAttributes.OnWaiting, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onWaiting' (receiver: obj, value: EventCallback<EventArgs>) =
        Attribute<KnownAttributes.OnWaiting, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onWaiting' (receiver: obj, value: Func<Task>) =
        Attribute<KnownAttributes.OnWaiting, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))

    static member onWaiting' (receiver: obj, value: Func<EventArgs, Task>) =
        Attribute<KnownAttributes.OnWaiting, _>(EventCallback.Factory.Create<EventArgs>(receiver, value))