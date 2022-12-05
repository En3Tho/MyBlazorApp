module En3Tho.FSharp.BlazorBuilder.CodeGeneration.ImportGenerator

open System
open System.Collections.Generic
open System.Reflection
open En3Tho.FSharp.ComputationExpressions.CodeBuilder
open FSharpComponents
open Microsoft.AspNetCore.Components
open Microsoft.AspNetCore.Components.QuickGrid

module ImportHelper =
    let genRawTypeName (type': Type) =
        if type'.IsGenericType then
            type'.Name[0..(type'.Name.IndexOf('`') - 1)]
        elif type'.IsGenericParameter then
            "'" + type'.Name
        else
            type'.Name

    let rec genTypeName (type': Type) =
        if type'.IsGenericType then
            let genericParameters = type'.GetGenericArguments()
            let rawTypeName = genRawTypeName type'
            let genericParameters = genericParameters |> Seq.map genTypeName |> String.concat ", "
            $"{rawTypeName}<{genericParameters}>"
        elif type'.IsGenericParameter then
            "'" + type'.Name
        else
            type'.Name

    let genImportModuleName (type': Type) =
        let rawTypeName = genRawTypeName type'
        let genericArguments = type'.GetGenericArguments()
        let typeName = if type'.IsGenericType then $"{rawTypeName}__{genericArguments.Length}" else rawTypeName
        $"{typeName}__Import"

    let genImportTypeName (type': Type) =
        if type'.IsGenericType then
            let genericArguments = type'.GetGenericArguments()
            let rawTypeName = genRawTypeName type'
            let genericArgumentsString = genericArguments |> Seq.map genTypeName |> String.concat ", "
            $"{rawTypeName}__{genericArguments.Length}__Import<{genericArgumentsString}>"
        else
            $"{type'.Name}__Import"

    let rec collectNamespacesForType (nsCache: HashSet<string>) (type': Type) =
        if type'.IsGenericParameter then
            ()
        elif type'.IsGenericType then
            let genericParameters = type'.GetGenericArguments()
            genericParameters |> Seq.iter (collectNamespacesForType nsCache)
            nsCache.Add type'.Namespace |> ignore
        else
            nsCache.Add type'.Namespace |> ignore


let genImportsModule (type': Type) =

    let isParameter (prop: PropertyInfo) =
        prop.GetCustomAttribute<ParameterAttribute>() <> null

    let isRequired (prop: PropertyInfo) =
        prop.GetCustomAttribute<EditorRequiredAttribute>() <> null

    let namespacesToOpen = HashSet()

    namespacesToOpen.Add(type'.Namespace) |> ignore
    let parameters =
        type'.GetProperties(BindingFlags.Public ||| BindingFlags.Instance)
        |> Seq.where isParameter
        |> Seq.map (fun param ->
            ImportHelper.collectNamespacesForType namespacesToOpen param.PropertyType
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
        |> Seq.map (fun prop -> $"{prop.Name.ToLower()}: {ImportHelper.genTypeName prop.PropertyType}")
        |> Seq.append [| "builder: BlazorBuilderCore" |]
        |> String.concat ", "

    let parameters =
        required
        |> Seq.map (fun prop -> prop.Name.ToLower())
        |> Seq.append [| "builder" |]
        |> String.concat ", "

    let importTypeName = ImportHelper.genImportTypeName type'
    let typeName = ImportHelper.genTypeName type'
    let moduleToOpen = ImportHelper.genImportModuleName type'

    code {
        "[<AutoOpen>]"
        $"module {moduleToOpen} ="
        indent {
            for ns in namespacesToOpen do
                $"open {ns}"
            ""
            $"type [<Struct; IsReadOnly>] {importTypeName}(builder: BlazorBuilderCore) ="
            indent {
                for optionalProperty in optional do
                    ""
                    $"member this.{optionalProperty.Name} with set(value: {ImportHelper.genTypeName optionalProperty.PropertyType}) ="
                    indent {
                        $"builder.AddAttribute(\"{optionalProperty.Name}\", value)"
                    }
                ""
                "interface IComponentImport with"
                indent {
                    "member _.Builder = builder"
                }
            }
            ""
            $"type {typeName} with"
            indent {
                $"static member inline Render({annotatedParameters}) ="
                indent {
                    $"builder.OpenComponent<{typeName}>()"
                    for requiredProperty in required do
                        $"builder.AddAttribute(\"{requiredProperty.Name}\", {requiredProperty.Name.ToLower()})"
                    $"{importTypeName}(builder)"
                }
            }
        }
    }

let genImportsForNamespace (rootNamespace: string) (types: Type[]) =
    code {
        $"namespace {rootNamespace}"
        "open System.Runtime.CompilerServices"
        "open En3Tho.FSharp.BlazorBuilder.Core"
        for type' in types do
            ""
            genImportsModule type'
    }

let genImportsForAssembly rootNamespace (assembly: Assembly) =
    let componentTypes =
        assembly.GetTypes()
        |> Seq.filter (fun type' ->
            type'.IsPublic && not type'.IsAbstract
            && type'.IsAssignableTo(typeof<ComponentBase>)
            && type'.Namespace.Equals(rootNamespace))
        |> Seq.toArray

    genImportsForNamespace rootNamespace componentTypes

let run() =
    genImportsForAssembly typeof<HelloWorldFSharp>.Namespace typeof<HelloWorldFSharp>.Assembly
    |> Code.writeToFile (typeof<HelloWorldFSharp>.Namespace.Replace(".", "") + ".Imports.fs")

let runQuickGrid() =
    genImportsForAssembly typedefof<QuickGrid<_>>.Namespace typedefof<QuickGrid<_>>.Assembly
    |> Code.writeToFile (typedefof<QuickGrid<_>>.Namespace.Replace(".", "") + ".Imports.fs")