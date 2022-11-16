module En3Tho.FSharp.BlazorBuilder.CodeGeneration.ImportGenerator

open System
open System.Collections.Generic
open System.Reflection
open En3Tho.FSharp.ComputationExpressions.CodeBuilder
open FSharpComponents
open Microsoft.AspNetCore.Components

// namespace TailwindComponents.CodinGame

// module ImportStubs =
    // open ... ?
    // type [<Struct; IsReadOnly>] MatrixImport(builder: BlazorBuilderCore) =
    //
    // interface IComponentImport with
    //     member _.Builder = builder

    // type Matrix with
    //     static member inline Render(builder: BlazorBuilderCore, data: int[][]) =
    //         builder.OpenComponent<Matrix>()
    //         builder.AddAttribute("Data", data)
    //         MatrixImport(builder)

// [<AbstractClass; Sealed; AutoOpen>]
// type Imports() =
    //static member inline Matrix'(builder, data) = Matrix.Render(builder, data)

let genImportStubAndCaller (nsCache: HashSet<string>) (type': Type) =

    let isParameter (prop: PropertyInfo) =
        prop.GetCustomAttribute<ParameterAttribute>() <> null

    let isRequired (prop: PropertyInfo) =
        prop.GetCustomAttribute<EditorRequiredAttribute>() <> null

    let parameters =
        type'.GetProperties(BindingFlags.Public ||| BindingFlags.Instance)
        |> Seq.where isParameter
        |> Seq.map (fun param ->
            nsCache.Add(param.PropertyType.Namespace) |> ignore
            param)
        |> Seq.toArray

    let required =
        parameters
        |> Seq.filter isRequired
        |> Seq.toArray

    let optional =
        parameters
        |> Seq.filter (not << isRequired)
        |> Seq.toArray

    let annotatedParameters =
        required
        |> Seq.map (fun prop -> $"{prop.Name.ToLower()}: {prop.PropertyType.Name}")
        |> String.concat ", "

    let parameters =
        required
        |> Seq.map (fun prop -> prop.Name.ToLower())
        |> String.concat ", "

    let importStubCode = code {
        indent {
            $"type [<Struct; IsReadOnly>] {type'.Name}Import(builder: BlazorBuilderCore) ="
            indent {
                ""
                for optionalProperty in optional do
                    nsCache.Add(optionalProperty.PropertyType.Namespace)

                    $"member this.{optionalProperty.Name} with set(value: {optionalProperty.PropertyType.Name}) ="
                    indent {
                        $"builder.AddAttribute({optionalProperty.Name}, value)"
                    }

                "interface IComponentImport with"
                indent {
                    "member _.Builder = builder"
                }
            }
            ""
            $"type {type'.Name} with"
            indent {
                $"static member inline Render(builder: BlazorBuilderCore, {annotatedParameters}) ="
                indent {
                    $"builder.OpenComponent<{type'.Name}>()"
                    for requiredProperty in required do
                        $"builder.AddAttribute({requiredProperty.Name}, {requiredProperty.Name.ToLower()})"
                    $"{type'.Name}Import(builder)"
                }
            }
        }
    }

    let callerCode = code {
        $"static member inline {type'.Name}'(builder, {parameters}) = {type'.Name}.Render(builder, {parameters})"
    }

    importStubCode, callerCode

let genImportsForNamespace (rootNamespace: string) (types: Type[]) =
    let callers = List()
    code {
        rootNamespace
        "module Imports ="
        indent {
            let nsCache = HashSet<string>()
            for type' in types do
                let importStubCode, callerCode = genImportStubAndCaller nsCache type'
                callers.Add(callerCode)
                for ns in nsCache do
                    $"open {ns}"
                ""
                importStubCode
        }
        ""
        "[<AbstractClass; Sealed; AutoOpen>]"
        "type Imports() ="
        indent {
            for caller in callers do
                ""
                caller
        }
    }

// TODO: can generate to different files etc

let genImportsForAssembly (rootNamespace: string) (assembly: Assembly) =
    let nsCache = HashSet<string>()
    let componentTypes =
        assembly.GetTypes()
        |> Seq.filter (fun type' ->
            not type'.IsAbstract
            && type'.IsAssignableFrom(typeof<ComponentBase>)
            && type'.Namespace.Equals(rootNamespace))
        |> Seq.toArray

    genImportsForNamespace rootNamespace componentTypes

let run() =
    genImportsForAssembly typeof<HelloWorldFSharp>.Namespace typeof<HelloWorldFSharp>.Assembly
    |> Code.writeToFile (typeof<HelloWorldFSharp>.Namespace.Replace(".", "") + ".fs")