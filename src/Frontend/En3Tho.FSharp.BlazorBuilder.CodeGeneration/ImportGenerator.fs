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
        let genericParameters = type'.GetGenericArguments()
        let typeName = if type'.IsGenericType then $"{rawTypeName}__{genericParameters.Length}" else rawTypeName
        $"{typeName}__Import"

    let genImportTypeName (type': Type) =
        if type'.IsGenericType then
            let genericParameters = type'.GetGenericArguments()
            let rawTypeName = genRawTypeName type'
            let genericParameters = genericParameters |> Seq.map genTypeName |> String.concat ", "
            $"{rawTypeName}Import<{genericParameters}>"
        else
            $"{type'.Name}Import"

    let rec collectNamespacesForType (nsCache: HashSet<string>) (type': Type) =
        if type'.IsGenericParameter then
            ()
        elif type'.IsGenericType then
            let genericParameters = type'.GetGenericArguments()
            genericParameters |> Seq.iter (collectNamespacesForType nsCache)
            nsCache.Add type'.Namespace |> ignore
        else
            nsCache.Add type'.Namespace |> ignore


let genImportStubAndCaller (nsCache: HashSet<string>) (type': Type) =

    let isParameter (prop: PropertyInfo) =
        prop.GetCustomAttribute<ParameterAttribute>() <> null

    let isRequired (prop: PropertyInfo) =
        prop.GetCustomAttribute<EditorRequiredAttribute>() <> null

    nsCache.Add(type'.Namespace) |> ignore
    let parameters =
        type'.GetProperties(BindingFlags.Public ||| BindingFlags.Instance)
        |> Seq.where isParameter
        |> Seq.map (fun param ->
            ImportHelper.collectNamespacesForType nsCache param.PropertyType
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

    let importStubCode = code {
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

    let callerCode = code {
        $"static member inline {ImportHelper.genRawTypeName type'}'({parameters}) = {typeName}.Render({parameters})"
    }

    importStubCode, callerCode

let genImportsForNamespace (rootNamespace: string) (types: Type[]) =
    let callers = List()
    let modulesToOpen = List()
    code {
        $"namespace {rootNamespace}.GeneratedImports"
        "open System.Runtime.CompilerServices"
        "open En3Tho.FSharp.BlazorBuilder.Core"
        for type' in types do
            let nsCache = HashSet<string>()
            let importStubCode, callerCode = genImportStubAndCaller nsCache type'
            let moduleToOpen = ImportHelper.genImportModuleName type'
            callers.Add(callerCode)
            modulesToOpen.Add(moduleToOpen)

            ""
            $"module {moduleToOpen} ="
            indent {
                for ns in nsCache do
                    $"open {ns}"
                ""
                importStubCode
            }
        ""
        $"open {rootNamespace}"
        for moduleToOpen in modulesToOpen do
            $"open {moduleToOpen}"
        ""
        "[<AbstractClass; Sealed; AutoOpen>]"
        "type ImportsAsMembers() ="
        indent {
            for caller in callers do
                ""
                caller
        }
    }

// TODO: generics

// TODO: can generate to different files etc

let genImportsForAssembly (rootNamespace: string) (assembly: Assembly) =
    let nsCache = HashSet<string>()
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