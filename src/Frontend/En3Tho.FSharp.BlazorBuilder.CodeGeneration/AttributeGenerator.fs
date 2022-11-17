module En3Tho.FSharp.BlazorBuilder.CodeGeneration.AttributeGenerator
open System
open En3Tho.FSharp.ComputationExpressions.CodeBuilder
open Microsoft.AspNetCore.Components
open Microsoft.AspNetCore.Components.Web

// IAvailableOn interface ?
// Any
// KnownElements?

// input for example should yield IAvailableOn<Input> | IAvailableOn<Any>

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
}

let genCallbackAttributeModule (attributesAndArgNames: (string * string) seq) = code {
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
            genCallbackAttribute attr argName
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

let htmlStringAttributes = [
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

let callbackAttributesAndArgNames = [
    "OnAbort", typeof<ProgressEventArgs>.Name
    "OnActivate", typeof<EventArgs>.Name
    "OnBeforeActivate", typeof<EventArgs>.Name
    "OnBeforeCopy", typeof<EventArgs>.Name
    "OnBeforeCut", typeof<EventArgs>.Name
    "OnBeforeDeactivate", typeof<EventArgs>.Name
    "OnBeforePaste", typeof<EventArgs>.Name
    "OnBlur", typeof<FocusEventArgs>.Name
    "OnCanPlay", typeof<EventArgs>.Name
    "OnCanPlayThrough", typeof<EventArgs>.Name
    "OnChange", typeof<ChangeEventArgs>.Name
    "OnClick", typeof<MouseEventArgs>.Name
    "OnContextMenu", typeof<MouseEventArgs>.Name
    "OnCopy", typeof<ClipboardEventArgs>.Name
    "OnCueChange", typeof<EventArgs>.Name
    "OnCut", typeof<ClipboardEventArgs>.Name
    "OnDblClick", typeof<MouseEventArgs>.Name
    "OnDeactivate", typeof<EventArgs>.Name
    "OnDrag", typeof<DragEventArgs>.Name
    "OnDragEnd", typeof<DragEventArgs>.Name
    "OnDragEnter", typeof<DragEventArgs>.Name
    "OnDragExit", typeof<DragEventArgs>.Name
    "OnDragLeave", typeof<DragEventArgs>.Name
    "OnDragOver", typeof<DragEventArgs>.Name
    "OnDragStart", typeof<DragEventArgs>.Name
    "OnDrop", typeof<DragEventArgs>.Name
    "OnDurationChange", typeof<EventArgs>.Name
    "OnEmptied", typeof<EventArgs>.Name
    "OnEnded", typeof<EventArgs>.Name
    "OnError", typeof<ErrorEventArgs>.Name
    "OnFocus", typeof<FocusEventArgs>.Name
    "OnFocusIn", typeof<FocusEventArgs>.Name
    "OnFocusOut", typeof<FocusEventArgs>.Name
    "OnFullscreenChange", typeof<EventArgs>.Name
    "OnFullscreenError", typeof<EventArgs>.Name
    "OnGotPointerCapture", typeof<PointerEventArgs>.Name
    "OnInput", typeof<ChangeEventArgs>.Name
    "OnInvalid", typeof<EventArgs>.Name
    "OnKeyDown", typeof<KeyboardEventArgs>.Name
    "OnKeyPress", typeof<KeyboardEventArgs>.Name
    "OnKeyUp", typeof<KeyboardEventArgs>.Name
    "OnLoad", typeof<ProgressEventArgs>.Name
    "OnLoadedData", typeof<EventArgs>.Name
    "OnLoadedMetadata", typeof<EventArgs>.Name
    "OnLoadEnd", typeof<ProgressEventArgs>.Name
    "OnLoadStart", typeof<ProgressEventArgs>.Name
    "OnLostPointerCapture", typeof<PointerEventArgs>.Name
    "OnMouseDown", typeof<MouseEventArgs>.Name
    "OnMouseEnter", typeof<MouseEventArgs>.Name
    "OnMouseLeave", typeof<MouseEventArgs>.Name
    "OnMouseMove", typeof<MouseEventArgs>.Name
    "OnMouseOut", typeof<MouseEventArgs>.Name
    "OnMouseOver", typeof<MouseEventArgs>.Name
    "OnMouseUp", typeof<MouseEventArgs>.Name
    "OnMouseWheel", typeof<WheelEventArgs>.Name
    "OnPaste", typeof<ClipboardEventArgs>.Name
    "OnPause", typeof<EventArgs>.Name
    "OnPlay", typeof<EventArgs>.Name
    "OnPlaying", typeof<EventArgs>.Name
    "OnPointerCancel", typeof<PointerEventArgs>.Name
    "OnPointerDown", typeof<PointerEventArgs>.Name
    "OnPointerEnter", typeof<PointerEventArgs>.Name
    "OnPointerLeave", typeof<PointerEventArgs>.Name
    "OnPointerLockChange", typeof<EventArgs>.Name
    "OnPointerLockError", typeof<EventArgs>.Name
    "OnPointerMove", typeof<PointerEventArgs>.Name
    "OnPointerOut", typeof<PointerEventArgs>.Name
    "OnPointerOver", typeof<PointerEventArgs>.Name
    "OnPointerUp", typeof<PointerEventArgs>.Name
    "OnProgress", typeof<ProgressEventArgs>.Name
    "OnRateChange", typeof<EventArgs>.Name
    "OnReadyStateChange", typeof<EventArgs>.Name
    "OnReset", typeof<EventArgs>.Name
    "OnScroll", typeof<EventArgs>.Name
    "OnSeeked", typeof<EventArgs>.Name
    "OnSeeking", typeof<EventArgs>.Name
    "OnSelect", typeof<EventArgs>.Name
    "OnSelectionChange", typeof<EventArgs>.Name
    "OnSelectStart", typeof<EventArgs>.Name
    "OnStalled", typeof<EventArgs>.Name
    "OnStop", typeof<EventArgs>.Name
    "OnSubmit", typeof<EventArgs>.Name
    "OnSuspend", typeof<EventArgs>.Name
    "OnTimeout", typeof<EventArgs>.Name
    "OnTimeUpdate", typeof<EventArgs>.Name
    "OnToggle", typeof<EventArgs>.Name
    "OnTouchCancel", typeof<TouchEventArgs>.Name
    "OnTouchEnd", typeof<TouchEventArgs>.Name
    "OnTouchEnter", typeof<TouchEventArgs>.Name
    "OnTouchLeave", typeof<TouchEventArgs>.Name
    "OnTouchMove", typeof<TouchEventArgs>.Name
    "OnTouchStart", typeof<TouchEventArgs>.Name
    "OnVolumeChange", typeof<EventArgs>.Name
    "OnWaiting", typeof<EventArgs>.Name
    "OnWheel", typeof<WheelEventArgs>.Name
]

let getKnownAttributes() =
    genKnownAttributeModule (List.sort [
        yield! (callbackAttributesAndArgNames |> Seq.map fst)
        yield! htmlStringAttributes
        yield! htmlBoolAttributes
        "ChildContent"
    ])
    |> Code.writeToFile "KnownAttributes.fs"

let genCallbackAttributes() =
    genCallbackAttributeModule callbackAttributesAndArgNames
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