module En3Tho.FSharp.BlazorBuilder.CodeGeneration.ElementGenerator
open En3Tho.FSharp.ComputationExpressions.CodeBuilder

let genKnownElement (elementName: string) = code {
    $"type [<Struct>] {elementName} ="
    indent {
        "interface IElementName with"
        indent {
            $"member _.Name = \"{elementName.ToLower()}\""
        }
    }
}

let genElementImportCE (elementName: string) = code {
    $"let {elementName.ToLower()} = ElementBlockBase<{elementName}>()"
}

let genKnownElementsModule (elements: string seq) = code {
    "module En3Tho.FSharp.BlazorBuilder.Core.KnownElements"
    for elementName in elements do
        ""
        genKnownElement elementName
}

let getElementsImportsModule (elements: string seq) = code {
    "module [<AutoOpen>] En3Tho.FSharp.BlazorBuilder.Elements"
    ""
    "open En3Tho.FSharp.BlazorBuilder.Core.KnownElements"
    for elementName in elements do
        ""
        genElementImportCE elementName
}

let knownElements = [
    "A"
    "Abbr"
    "Address"
    "Area"
    "Article"
    "Aside"
    "Audio"
    "B"
    "Base'" // special case base as it's an F# keyword
    "Bdi"
    "Bdo"
    "Blockquote"
    "Body"
    "Br"
    "Button"
    "Canvas"
    "Caption"
    "Cite"
    "Code"
    "Col"
    "Colgroup"
    "Data"
    "Datalist"
    "Dd"
    "Del"
    "Details"
    "Dfn"
    "Dialog"
    "Div"
    "Dl"
    "Dt"
    "Em"
    "Embed"
    "Fieldset"
    "Figcaption"
    "Figure"
    "Footer"
    "Form"
    "H1"
    "H2"
    "H3"
    "H4"
    "H5"
    "H6"
    "Head"
    "Header"
    "Hgroup"
    "Hr"
    "Html"
    "I"
    "Iframe"
    "Img"
    "Input"
    "Ins"
    "Kbd"
    "Label"
    "Legend"
    "Li"
    "Link"
    "Main"
    "Map"
    "Mark"
    "Menu"
    "Meta"
    "Meter"
    "Nav"
    "Noscript"
    "Object"
    "Ol"
    "Optgroup"
    "Option"
    "Output"
    "P"
    "Picture"
    "Pre"
    "Progress"
    "Q"
    "Rp"
    "Rt"
    "Ruby"
    "S"
    "Samp"
    "Script"
    "Section"
    "Select"
    "Slot"
    "Small"
    "Source"
    "Span"
    "Strong"
    "Style"
    "Sub"
    "Summary"
    "Sup"
    "Table"
    "Tbody"
    "Td"
    "Template"
    "Textarea"
    "Tfoot"
    "Th"
    "Thead"
    "Time"
    "Title"
    "Tr"
    "Track"
    "U"
    "Ul"
    "Var"
    "Video"
    "Wbr"
]

let run() =
    getElementsImportsModule knownElements
    |> Code.writeToFile "Elements.fs"

    genKnownElementsModule knownElements
    |> Code.writeToFile "KnownElements.fs"