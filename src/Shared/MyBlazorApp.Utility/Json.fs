module MyBlazorApp.Utility.Json

open System.Text.Json
open System.Text.Json.Serialization

[<CompiledName("AddFSharpConverters")>]
let addFSharpConverters (options: JsonSerializerOptions) =
    options.PropertyNameCaseInsensitive <- true
    options.PropertyNamingPolicy <- JsonNamingPolicy.CamelCase
    options.Converters.Add(JsonFSharpConverter(JsonUnionEncoding.UnwrapSingleFieldCases
                                           ||| JsonUnionEncoding.UnwrapSingleCaseUnions
                                           ||| JsonUnionEncoding.UnwrapFieldlessTags
                                           ||| JsonUnionEncoding.UnwrapOption))
    options

[<CompiledName("CreateDefaultOptions")>]
let createDefaultOptions() =
    let options = JsonSerializerOptions()
    addFSharpConverters options