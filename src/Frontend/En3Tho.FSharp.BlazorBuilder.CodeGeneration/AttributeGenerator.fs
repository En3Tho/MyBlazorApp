module En3Tho.FSharp.BlazorBuilder.CodeGeneration.AttributeGenerator
open System
open System.Collections.Generic
open En3Tho.FSharp.ComputationExpressions.CodeBuilder
open Microsoft.AspNetCore.Components
open Microsoft.AspNetCore.Components.Web

let genLowercasedAttributeName (name: string) =
    let name = name.Replace("-", "")
    $"{Char.ToLower(name[0])}{name[1..]}'"

let genKnownAttributeName (name: string) =
    name.Replace("-", "")

let genKnownAttribute (attr: string) = code {
    let attrName = genKnownAttributeName attr

    $"type [<Struct>] {attrName} ="
    indent {
        "interface IAttributeName with"
        indent {
            $"member _.Name = \"{attrName.ToLower()}\""
        }
    }
}

let genKnownAttributeModule (attributes: string seq) = code {
    "module En3Tho.FSharp.BlazorBuilder.Core.KnownAttributes"
    for attr in attributes do
        ""
        genKnownAttribute attr
}

let genCallbackAttribute (name: string) (argsName: string) = code {
    let memberName = genLowercasedAttributeName name
    let attrName = genKnownAttributeName name

    for param in [|
        "Action"
        $"Action<{argsName}>"
        "EventCallback"
        $"EventCallback<{argsName}>"
        "Func<Task>"
        $"Func<{argsName}, Task>"
    |] do
        $"static member {memberName} (receiver: obj, value: {param}) ="
        indent {
            $"Attribute<KnownAttributes.{attrName}, _>(EventCallback.Factory.Create<{argsName}>(receiver, value))"
        }
        ""
    trimEnd()
}

let genCallbackAttributeModule (attributesAndArgNames: (string * Type) seq) = code {
    "namespace En3Tho.FSharp.BlazorBuilder"

    "open System"
    "open System.Threading.Tasks"
    "open En3Tho.FSharp.BlazorBuilder.Core"
    "open Microsoft.AspNetCore.Components"
    "open Microsoft.AspNetCore.Components.Web"
    ""
    "[<AbstractClass; Sealed; AutoOpen>]"
    "type CallbackAttributes() ="
    indent {
        for attr, argName in attributesAndArgNames do
            ""
            genCallbackAttribute attr argName.Name
    }
}

let genStringAttribute (name: string) = code {
    let memberName = genLowercasedAttributeName name
    let attrName = genKnownAttributeName name
    $"static member {memberName} (value: string) = Attribute<KnownAttributes.{attrName}, _>(value)"
}

let genBoolAttribute (name: string) = code {
    let memberName = genLowercasedAttributeName name
    let attrName = genKnownAttributeName name
    $"static member {memberName} = Attribute<KnownAttributes.{attrName}>()"
}

let genInputTypeAttribute (name: string) = code {
    let attrName = genKnownAttributeName name
    $"static member type{attrName}' = Attribute<KnownAttributes.Type, _>(\"{name.ToLower()}\")"
}

let genStringAttributeModule (attributes: string seq) = code {
    "namespace En3Tho.FSharp.BlazorBuilder"
    ""
    "open En3Tho.FSharp.BlazorBuilder.Core"
    ""
    "[<AbstractClass; Sealed; AutoOpen>]"
    "type StringAttributes() ="
    indent {
        for attr in attributes do
            ""
            genStringAttribute attr
    }
}

let genBoolAttributeModule (attributes: string seq) = code {
    "namespace En3Tho.FSharp.BlazorBuilder"
    ""
    "open En3Tho.FSharp.BlazorBuilder.Core"
    ""
    "[<AbstractClass; Sealed; AutoOpen>]"
    "type BoolAttributes() ="
    indent {
        for attr in attributes do
            ""
            genBoolAttribute attr
    }
}

let getInputTypesModule (inputTypes: string seq) = code {
    "namespace En3Tho.FSharp.BlazorBuilder"
    ""
    "open En3Tho.FSharp.BlazorBuilder.Core"
    ""
    "[<AbstractClass; Sealed; AutoOpen>]"
    "type InputTypes() ="
    indent {
        for inputType in inputTypes do
            ""
            genInputTypeAttribute inputType
    }
}

let inputTypes = [
    "Hidden"
    "Search"
    "Text"
    "Tel"
    "Url"
    "Email"
    "Password"
    "Date"
    "Month"
    "Week"
    "Time"
    "Datetime-Local"
    "Number"
    "Range"
    "Color"
    "Checkbox"
    "Radio"
    "File"
    "Submit"
    "Image"
    "Reset"
    "Button"
]

let htmlBoolAttributes = [
    "AllowFullScreen"
    "AllowPaymentRequest"
    "Async"
    "Autofocus"
    "Autoplay"
    "Checked"
    "Controls"
    "Default"
    "Defer"
    "Disabled"
    "Formnovalidate"
    "Hidden"
    "Ismap"
    "ItemScope"
    "Loop"
    "Multiple"
    "Muted"
    "NoModule"
    "NoValidate"
    "Open"
    "Readonly"
    "Required"
    "Reversed"
    "Selected"
]

let htmlStringAttributes = [ // Maybe generate those for components that explicitly accept them? Like IHtmlElementAttribute, IDivAttribute, IInputAttribute, etc.
    "Accept"
    "AcceptCharset"
    "AccessKey"
    "Action"
    "Alt"
    "AutoCapitalize"
    "AutoComplete"
    "AutoCorrect"
    "AutoSave"
    "Capture"
    "Charset"
    "Challenge"
    "Class"
    "Color"
    "Cols"
    "Content"
    "ContentEditable"
    "ContextMenu"
    "ControlsList"
    "Coords"
    "CrossOrigin"
    "Csp"
    "DateTime"
    "Dir"
    "Download"
    "Draggable"
    "EncType"
    "EnterKeyHint"
    "For"
    "Form"
    "FormAction"
    "FormEncType"
    "FormMethod"
    "FormNoValidate"
    "FormTarget"
    "Headers"
    "Height"
    "High"
    "Href"
    "HrefLang"
    "HttpEquiv"
    "Icon"
    "Id"
    "Importance"
    "Integrity"
    "InputMode"
    "Is"
    "KeyParams"
    "Keytype"
    "Kind"
    "Label"
    "Lang"
    "List"
    "Loading"
    "Low"
    "Manifest"
    "Max"
    "MaxLength"
    "Media"
    "Method"
    "Min"
    "MinLength"
    "Name"
    "Nonce"
    "Optimum"
    "Pattern"
    "Placeholder"
    "Poster"
    "Preload"
    "Radiogroup"
    "ReferrerPolicy"
    "Rel"
    "Render"
    "Role"
    "Rows"
    "RowSpan"
    "Scope"
    "Scoped"
    "Shape"
    "Size"
    "Sizes"
    "Slot"
    "Span"
    "SpellCheck"
    "Src"
    "SrcDoc"
    "SrcLang"
    "SrcSet"
    "Start"
    "Step"
    "Style"
    "Summary"
    "TabIndex"
    "Target"
    "Title"
    "Translate"
    "Type"
    "UseMap"
    "Value"
    "Width"
    "Wrap"
]

let argsTypeToCallbackNames = Dictionary (seq {
    KeyValuePair(typeof<ProgressEventArgs>, [|
        "OnAbort"
        "OnLoad"
        "OnLoadEnd"
        "OnLoadStart"
        "OnProgress"
    |])
    
    KeyValuePair(typeof<FocusEventArgs>, [|
        "OnBlur"
        "OnFocus"
        "OnFocusIn"
        "OnFocusOut"
    |])
    
    KeyValuePair(typeof<ChangeEventArgs>, [|
        "OnInput"
        "OnChange"
    |])
    
    KeyValuePair(typeof<MouseEventArgs>, [|
        "OnClick"
        "OnContextMenu"
        "OnMouseDown"
        "OnMouseEnter"
        "OnMouseLeave"
        "OnMouseMove"
        "OnMouseOut"
        "OnMouseOver"
        "OnMouseUp"
        "OnDblClick"
    |])
    
    KeyValuePair(typeof<ClipboardEventArgs>, [|
        "OnCopy"
        "OnCut"
        "OnPaste"
    |])
    
    KeyValuePair(typeof<DragEventArgs>, [|
        "OnDrag"
        "OnDragEnd"
        "OnDragEnter"
        "OnDragExit"
        "OnDragLeave"
        "OnDragOver"
        "OnDragStart"
        "OnDrop"
    |])
    
    KeyValuePair(typeof<PointerEventArgs>, [|
        "OnGotPointerCapture"
        "OnLostPointerCapture"
        "OnPointerCancel"
        "OnPointerDown"
        "OnPointerEnter"
        "OnPointerLeave"
        "OnPointerMove"
        "OnPointerOut"
        "OnPointerOver"
        "OnPointerUp"
    |])
    
    KeyValuePair(typeof<KeyboardEventArgs>, [|
        "OnKeyDown"
        "OnKeyPress"
        "OnKeyUp"
    |])
    
    KeyValuePair(typeof<TouchEventArgs>, [|
        "OnTouchCancel"
        "OnTouchEnd"
        "OnTouchEnter"
        "OnTouchLeave"
        "OnTouchMove"
        "OnTouchStart"
    |])
    
    KeyValuePair(typeof<ErrorEventArgs>, [|
        "OnError"
    |])
    
    KeyValuePair(typeof<WheelEventArgs>, [|
        "OnLoadedMetadata"
        "OnMouseWheel"
        "OnWheel"
    |])
    
    KeyValuePair(typeof<EventArgs>, [|
        "OnActivate"
        "OnBeforeActivate"
        "OnBeforeCopy"
        "OnBeforeCut"
        "OnBeforeDeactivate"
        "OnBeforePaste"
        "OnCanPlay"
        "OnCanPlayThrough"
        "OnCueChange"
        "OnDeactivate"
        "OnDurationChange"
        "OnEmptied"
        "OnEnded"
        "OnFullscreenChange"
        "OnFullscreenError"
        "OnInvalid"
        "OnLoadedData"
        "OnPause"
        "OnPlay"
        "OnPlaying"
        "OnPointerLockChange"
        "OnPointerLockError"
        "OnRateChange"
        "OnReadyStateChange"
        "OnReset"
        "OnScroll"
        "OnSeeked"
        "OnSeeking"
        "OnSelect"
        "OnSelectionChange"
        "OnSelectStart"
        "OnStalled"
        "OnStop"
        "OnSubmit"
        "OnSuspend"
        "OnTimeout"
        "OnTimeUpdate"
        "OnToggle"
        "OnVolumeChange"
        "OnWaiting"
    |])
})
let getKnownAttributes() =
    genKnownAttributeModule (List.sort [
        yield! (argsTypeToCallbackNames.Values |> Seq.concat)
        yield! htmlStringAttributes
        yield! htmlBoolAttributes
        "ChildContent"
    ])
    |> Code.writeToFile "KnownAttributes.fs"

let genCallbackAttributes() =
    genCallbackAttributeModule (argsTypeToCallbackNames |> Seq.map (fun kvp -> kvp.Value |> Seq.map (fun value -> value, kvp.Key)) |> Seq.concat)
    |> Code.writeToFile "CallbackAttributes.fs"

let genStringAttributes() =
    genStringAttributeModule (htmlStringAttributes |> List.except [ "Class" ]) // special case class
    |> Code.writeToFile "StringAttributes.fs"

let genBoolAttributes() =
    genBoolAttributeModule htmlBoolAttributes
    |> Code.writeToFile "BoolAttributes.fs"

let genInputTypes() =
    getInputTypesModule inputTypes
    |> Code.writeToFile "InputTypes.fs"

let run() =
    getKnownAttributes()
    genCallbackAttributes()
    genStringAttributes()
    genBoolAttributes()
    genInputTypes()