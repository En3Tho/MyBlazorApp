module En3Tho.FSharp.BlazorBuilder.CodeGeneration.Code

open System.IO
open En3Tho.FSharp.ComputationExpressions.CodeBuilder.CodeBuilderImpl
open En3Tho.FSharp.ComputationExpressions.CodeBuilder

let writeToFile fileName (builder: CodeBuilder) =
    code {
        "// Auto-generated"
        builder
    }
    |> fun code -> code.ToString()
    |> fun text -> File.WriteAllText(fileName, text)