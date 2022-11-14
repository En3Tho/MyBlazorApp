namespace rec En3Tho.FSharp.ComputationExpressions.BlazorBuilder.Core

open System
open System.Runtime.CompilerServices
open En3Tho.FSharp.ComputationExpressions.BlazorBuilder.Core
open En3Tho.FSharp.Extensions.GenericBuilderBase
open Microsoft.AspNetCore.Components

type BlazorBuilderCode = BlazorBuilderCore -> unit
type BlazorBuilderAttributeCode = BlazorBuilderCore -> unit -> unit // erm
type ComponentImportCode<'a when 'a: struct and 'a :> IComponentImport> = BlazorBuilderCore -> 'a

type RenderTreeBlockBase() =
    inherit UnitBuilderBase<BlazorBuilderCore>()

    // This is needed to preserve correct sequence counts in RenderTreeBuilder in case of non-yield operations (eg if ... then ...)
    // I'm not sure how does this work with while or for
    // maybe this isn't really needed? Need to check out diff mechanism
    member inline _.Zero() : BlazorBuilderCode =
        fun builder ->
            builder.Advance()

    member inline _.Yield([<InlineIfLambda>] codeBuilderCode: BlazorBuilderCode) : BlazorBuilderCode =
        fun builder ->
            codeBuilderCode builder

    member inline _.Yield([<InlineIfLambda>] attributeCode: BlazorBuilderAttributeCode) : BlazorBuilderCode =
        fun builder ->
            attributeCode builder ()

    member inline _.Yield([<InlineIfLambda>] codeBuilderCode: ComponentImportCode<_>) : BlazorBuilderCode =
        fun builder ->
            codeBuilderCode builder |> ignore
            builder.CloseComponent()

    member inline _.Yield(fragment: RenderFragment) : BlazorBuilderCode =
        fun builder ->
            builder.AddContent(fragment)

    member inline _.Yield(content: string) : BlazorBuilderCode =
        fun builder ->
            builder.AddContent(content: string)

    member inline _.Yield(markup: MarkupString) : BlazorBuilderCode =
        fun builder ->
            builder.AddMarkupContent(markup.Value)

    member inline _.Yield(data, value: 'a) : BlazorBuilderCode =
        fun builder ->
            builder.AddAttribute(data, value)

    member inline _.Yield(_: ComponentBlock<'a>) : BlazorBuilderCode =
        fun builder ->
            builder.OpenComponent<'a>()
            builder.CloseComponent()

    member inline _.Yield(value: ElementBlockBase<'name>) : BlazorBuilderCode =
        fun builder ->
            builder.OpenElement(value.Name)
            builder.CloseElement()

type ElementBlockBase<'name when 'name :> IElementName and 'name: struct>() =
    inherit RenderTreeBlockBase()

    member _.Name = Unchecked.defaultof<'name>.Name

    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderCode) : BlazorBuilderCode =
        fun builder ->
            builder.OpenElement(this.Name)
            runExpr builder
            builder.CloseElement()

[<Sealed>]
type AttributeBlock() =
    inherit UnitBuilderBase<BlazorBuilderCore>()

    member inline _.Yield<'attr when 'attr: struct and 'attr :> IAttribute>(attr: 'attr) : BlazorBuilderCode =
        fun builder ->
            attr.RenderTo builder

    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderCode) : BlazorBuilderAttributeCode =
        fun builder _ ->
            runExpr builder

[<Sealed>]
type ComponentBlock<'a when 'a :> ComponentBase>() =
    inherit UnitBuilderBase<BlazorBuilderCore>()

    member inline _.Yield<'attr when 'attr: struct and 'attr :> IAttribute>(attr: 'attr) : BlazorBuilderCode =
        fun builder ->
            attr.RenderTo builder

    static member val Instance = ComponentBlock<'a>()

    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderCode) : BlazorBuilderCode =
        fun builder ->
            builder.OpenComponent<'a>()
            runExpr builder
            builder.CloseComponent()




[<AbstractClass; Sealed; Extension>]
type ComponentImportBlock() =

    [<Extension>]
    static member inline While(_: #IComponentImport, [<InlineIfLambda>] moveNext: unit -> bool, [<InlineIfLambda>] whileExpr: BlazorBuilderCode) : BlazorBuilderCode =
        fun builder -> while moveNext() do (whileExpr(builder))

    [<Extension>]
    static member inline Combine(_: #IComponentImport, [<InlineIfLambda>] first: BlazorBuilderCode, [<InlineIfLambda>] second: BlazorBuilderCode) : BlazorBuilderCode =
        fun(builder) ->
            first(builder)
            second(builder)

    [<Extension>]
    static member inline TryFinally(_: #IComponentImport, [<InlineIfLambda>] tryExpr: BlazorBuilderCode, [<InlineIfLambda>] compensation: BlazorBuilderCode) =
        fun (builder) ->
            try
                tryExpr(builder)
            finally
                compensation(builder)

    [<Extension>]
    static member inline TryWith(_: #IComponentImport, [<InlineIfLambda>] tryExpr, [<InlineIfLambda>] compensation: exn -> BlazorBuilderCode) : BlazorBuilderCode =
        fun (builder) ->
            try
                tryExpr()
            with e ->
                (compensation e)(builder)

    [<Extension>]
    static member inline Using(this: #IComponentImport, resource: #IDisposable, [<InlineIfLambda>] tryExpr: #IDisposable -> BlazorBuilderCode) : BlazorBuilderCode =
        this.TryFinally(
            (fun (builder) -> (tryExpr(resource)(builder))),
            (fun (builder) -> if not (isNull (box resource)) then resource.Dispose()))

    [<Extension>]
    static member inline For(this: #IComponentImport, values: 'a seq, [<InlineIfLambda>] forExpr: 'a -> BlazorBuilderCode) : BlazorBuilderCode =
        this.Using(
            values.GetEnumerator(), (fun e ->
                this.While((fun () -> e.MoveNext()), (fun (builder) ->
                    (forExpr e.Current)(builder))
                )
            )
        )

    [<Extension>]
    static member inline Zero(_: #IComponentImport) : BlazorBuilderCode = fun (builder) -> ()

    [<Extension>]
    static member inline Delay(_: #IComponentImport, [<InlineIfLambda>] delay: unit -> BlazorBuilderCode) =
        fun (builder) -> (delay())(builder)

    [<Extension>]
    static member inline Yield<'attr, 'import when 'attr: struct and 'attr :> IAttribute and 'import :> IComponentImport>(_: 'import, attr: 'attr) =
        fun builder ->
            attr.RenderTo builder

    [<Extension>]
    static member inline Run(this: #IComponentImport, [<InlineIfLambda>] runExpr: BlazorBuilderCode) =
        runExpr this.Builder
        this

[<Sealed>]
type BlazorBuilderRunner() =

    inherit RenderTreeBlockBase()
    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderCode) =
        fun builder ->
            let builder = BlazorBuilderCore(builder)
            runExpr builder

[<Sealed>]
type RenderFragmentRunner() =

    inherit RenderTreeBlockBase()
    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderCode) : RenderFragment =
        RenderFragment(fun builder -> runExpr (BlazorBuilderCore(builder)))