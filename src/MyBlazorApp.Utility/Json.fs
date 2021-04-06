module MyBlazorApp.Utility.Json

open System.Text.Json
open System.Text.Json.Serialization

[<CompiledName("UpdateExistingOptions")>]
let updateExistingOptions (options: JsonSerializerOptions) =
    options.PropertyNameCaseInsensitive <- true
    options.PropertyNamingPolicy <- JsonNamingPolicy.CamelCase
    options.Converters.Add(JsonFSharpConverter(JsonUnionEncoding.UnwrapSingleFieldCases
                                           ||| JsonUnionEncoding.UnwrapSingleCaseUnions
                                           ||| JsonUnionEncoding.UnwrapFieldlessTags
                                           ||| JsonUnionEncoding.UnwrapOption))

[<CompiledName("CreateDefaultOptions")>]
let createDefaultOptions() =
    let options = JsonSerializerOptions()
    updateExistingOptions options
    options