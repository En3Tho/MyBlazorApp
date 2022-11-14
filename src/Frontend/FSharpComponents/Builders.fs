namespace rec En3Tho.FSharp.ComputationExpressions.BlazorBuilder

open System
open System.Collections.Generic
open System.Runtime.CompilerServices
open En3Tho.FSharp.ComputationExpressions.BlazorBuilder.Core
open Microsoft.AspNetCore.Components

type BlazorBuilderElementCode = delegate of BlazorBuilderCore -> unit
type BlazorBuilderComponentCode = BlazorBuilderCore -> unit -> unit
type ComponentImportCode<'a when 'a: struct and 'a :> IComponentImport> = BlazorBuilderCore -> 'a

type BlazorBuilderBase() =
    member inline this.While([<InlineIfLambda>] moveNext: unit -> bool, [<InlineIfLambda>] whileExpr: BlazorBuilderElementCode) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun builder -> while moveNext() do (whileExpr.Invoke(builder)))

    member inline this.Combine([<InlineIfLambda>] first: BlazorBuilderElementCode, [<InlineIfLambda>] second: BlazorBuilderElementCode) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    member inline this.TryFinally([<InlineIfLambda>] tryExpr: BlazorBuilderElementCode, [<InlineIfLambda>] compensation: BlazorBuilderElementCode) =
        BlazorBuilderElementCode(fun (builder) ->
            try
                tryExpr.Invoke(builder)
            finally
                compensation.Invoke(builder))

    member inline this.TryWith([<InlineIfLambda>] tryExpr, [<InlineIfLambda>] compensation: exn -> BlazorBuilderElementCode) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun (builder) ->
            try
                tryExpr()
            with e ->
                (compensation e).Invoke(builder))

    member inline this.Using(resource: #IDisposable, [<InlineIfLambda>] tryExpr: #IDisposable -> BlazorBuilderElementCode) : BlazorBuilderElementCode =
        this.TryFinally(
            (fun (builder) -> (tryExpr(resource).Invoke(builder))),
            (fun (builder) -> if not (isNull (box resource)) then resource.Dispose()))

    member inline this.For(values: 'a seq, [<InlineIfLambda>] forExpr: 'a -> BlazorBuilderElementCode) : BlazorBuilderElementCode =
        this.Using(
            values.GetEnumerator(), (fun (e: IEnumerator<'a>) ->
                this.While((fun () -> e.MoveNext()), (fun (builder) ->
                    (forExpr e.Current).Invoke(builder))
                )
            )
        )

    // This is needed to preserve correct sequence counts in RenderTreeBuilder in case of non-yield operations (eg if ... then ...)
    // I'm not sure how does this work with while or for
    // maybe this isn't really needed? Need to check out diff mechanism
    member inline _.Zero() : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun builder ->
            builder.Advance())

    member inline this.Delay([<InlineIfLambda>] delay: unit -> BlazorBuilderElementCode) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun (builder) -> (delay()).Invoke(builder))

type RenderTreeBlockBase() =
    inherit BlazorBuilderBase()

    member inline _.Yield([<InlineIfLambda>] codeBuilderCode: BlazorBuilderElementCode) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun builder ->
            codeBuilderCode.Invoke builder
            builder.CloseElement())

    member inline _.Yield([<InlineIfLambda>] codeBuilderCode: BlazorBuilderComponentCode) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun builder ->
            codeBuilderCode builder ())

    member inline _.Yield([<InlineIfLambda>] codeBuilderCode: ComponentImportCode<_>) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun builder ->
            codeBuilderCode builder |> ignore
            builder.CloseComponent())

    member inline _.Yield(fragment: RenderFragment) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun builder ->
            builder.AddContent(fragment))

    member inline _.Yield(content: string) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun builder ->
            builder.AddContent(content: string))

    member inline _.Yield(markup: MarkupString) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun builder ->
            builder.AddMarkupContent(markup.Value))

    member inline _.Yield(_: ComponentBlock<'a>) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun builder ->
            builder.OpenComponent<'a>()
            builder.CloseComponent())

[<Sealed>]
type ComponentBlock<'a when 'a :> ComponentBase>() =
    inherit BlazorBuilderBase()

    member inline _.Yield<'attr when 'attr: struct and 'attr :> IAttribute>(attr: 'attr) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun builder ->
            attr.RenderTo builder)

    static member val Instance = ComponentBlock<'a>()

    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderElementCode) : BlazorBuilderComponentCode =
        fun builder _ ->
            builder.OpenComponent<'a>()
            runExpr.Invoke builder
            builder.CloseComponent()

type ElementBlockBase<'name when 'name :> IElementName and 'name: struct>() =
    inherit BlazorBuilderBase()
    member _.Name = Unchecked.defaultof<'name>.Name

    member inline this.Yield<'attr, 'import when 'attr: struct and 'attr :> IAttribute and 'import :> IComponentImport>(_: 'import, attr: 'attr) =
        fun builder ->
            attr.RenderTo builder

    member inline _.Yield<'attr when 'attr: struct and 'attr :> IAttribute>(attr: 'attr) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun builder ->
            attr.RenderTo builder)

    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderElementCode) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun builder ->
            builder.OpenElement(this.Name)
            runExpr.Invoke builder)

[<AbstractClass; Sealed; Extension>]
type ComponentImportBlock() =

    [<Extension>]
    static member inline While(_: #IComponentImport, [<InlineIfLambda>] moveNext: unit -> bool, [<InlineIfLambda>] whileExpr: BlazorBuilderElementCode) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun builder -> while moveNext() do (whileExpr.Invoke(builder)))

    [<Extension>]
    static member inline Combine(_: #IComponentImport, [<InlineIfLambda>] first: BlazorBuilderElementCode, [<InlineIfLambda>] second: BlazorBuilderElementCode) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    [<Extension>]
    static member inline TryFinally(_: #IComponentImport, [<InlineIfLambda>] tryExpr: BlazorBuilderElementCode, [<InlineIfLambda>] compensation: BlazorBuilderElementCode) =
        BlazorBuilderElementCode(fun (builder) ->
            try
                tryExpr.Invoke(builder)
            finally
                compensation.Invoke(builder))

    [<Extension>]
    static member inline TryWith(_: #IComponentImport, [<InlineIfLambda>] tryExpr, [<InlineIfLambda>] compensation: exn -> BlazorBuilderElementCode) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun (builder) ->
            try
                tryExpr()
            with e ->
                (compensation e).Invoke(builder))

    [<Extension>]
    static member inline Using(this: #IComponentImport, resource: #IDisposable, [<InlineIfLambda>] tryExpr: #IDisposable -> BlazorBuilderElementCode) : BlazorBuilderElementCode =
        this.TryFinally(
            (fun (builder) -> (tryExpr(resource).Invoke(builder))),
            (fun (builder) -> if not (isNull (box resource)) then resource.Dispose()))

    [<Extension>]
    static member inline For(this: #IComponentImport, values: 'a seq, [<InlineIfLambda>] forExpr: 'a -> BlazorBuilderElementCode) : BlazorBuilderElementCode =
        this.Using(
            values.GetEnumerator(), (fun (e: IEnumerator<'a>) ->
                this.While((fun () -> e.MoveNext()), (fun (builder) ->
                    (forExpr e.Current).Invoke(builder))
                )
            )
        )

    [<Extension>]
    static member inline Zero(_: #IComponentImport) : BlazorBuilderElementCode = BlazorBuilderElementCode(fun (builder) -> builder.Advance())

    [<Extension>]
    static member inline Delay(_: #IComponentImport, [<InlineIfLambda>] delay: unit -> BlazorBuilderElementCode) =
        fun (builder) -> (delay()).Invoke(builder)

    [<Extension>]
    static member inline Yield<'attr, 'import when 'attr: struct and 'attr :> IAttribute and 'import :> IComponentImport>(_: 'import, attr: 'attr) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun builder ->
            attr.RenderTo builder)

    [<Extension>]
    static member inline Run(this: #IComponentImport, [<InlineIfLambda>] runExpr: BlazorBuilderElementCode) =
        runExpr.Invoke this.Builder
        this

[<AbstractClass; Sealed; Extension>]
type BlazorBuilderCodeBlock() =

    [<Extension>]
    static member inline While([<InlineIfLambda>] this: BlazorBuilderElementCode, [<InlineIfLambda>] moveNext: unit -> bool, [<InlineIfLambda>] whileExpr: BlazorBuilderElementCode) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun builder -> while moveNext() do (whileExpr.Invoke(builder)))

    [<Extension>]
    static member inline Combine([<InlineIfLambda>] this: BlazorBuilderElementCode, [<InlineIfLambda>] first: BlazorBuilderElementCode, [<InlineIfLambda>] second: BlazorBuilderElementCode) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    [<Extension>]
    static member inline TryFinally([<InlineIfLambda>] this: BlazorBuilderElementCode, [<InlineIfLambda>] tryExpr: BlazorBuilderElementCode, [<InlineIfLambda>] compensation: BlazorBuilderElementCode) =
        BlazorBuilderElementCode(fun (builder) ->
            try
                tryExpr.Invoke(builder)
            finally
                compensation.Invoke(builder))

    [<Extension>]
    static member inline TryWith([<InlineIfLambda>] this: BlazorBuilderElementCode, [<InlineIfLambda>] tryExpr, [<InlineIfLambda>] compensation: exn -> BlazorBuilderElementCode) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun (builder) ->
            try
                tryExpr()
            with e ->
                (compensation e).Invoke(builder))

    [<Extension>]
    static member inline Using([<InlineIfLambda>] this: BlazorBuilderElementCode, resource: #IDisposable, [<InlineIfLambda>] tryExpr: #IDisposable -> BlazorBuilderElementCode) : BlazorBuilderElementCode =
        this.TryFinally(
            (fun (builder) -> (tryExpr(resource).Invoke(builder))),
            (fun (builder) -> if not (isNull (box resource)) then resource.Dispose()))

    [<Extension>]
    static member inline For([<InlineIfLambda>] this: BlazorBuilderElementCode, values: 'a seq, [<InlineIfLambda>] forExpr: 'a -> BlazorBuilderElementCode) : BlazorBuilderElementCode =
        this.Using(
            values.GetEnumerator(), (fun (e: IEnumerator<'a>) ->
                this.While((fun () -> e.MoveNext()), (fun (builder) ->
                    (forExpr e.Current).Invoke(builder))
                )
            )
        )

    [<Extension>]
    static member inline Zero([<InlineIfLambda>] this: BlazorBuilderElementCode) : BlazorBuilderElementCode = BlazorBuilderElementCode(fun (builder) -> builder.Advance())

    [<Extension>]
    static member inline Delay([<InlineIfLambda>] this: BlazorBuilderElementCode, [<InlineIfLambda>] delay: unit -> BlazorBuilderElementCode) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun (builder) -> (delay()).Invoke(builder))
    
    [<Extension>]
    static member inline Yield([<InlineIfLambda>] this: BlazorBuilderElementCode, [<InlineIfLambda>] codeBuilderCode: BlazorBuilderElementCode) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun builder ->
            codeBuilderCode.Invoke builder)

    [<Extension>]
    static member inline Yield([<InlineIfLambda>] this: BlazorBuilderElementCode, [<InlineIfLambda>] codeBuilderCode: ComponentImportCode<_>) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun builder ->
            codeBuilderCode builder |> ignore
            builder.CloseComponent())

    [<Extension>]
    static member inline Yield([<InlineIfLambda>] this: BlazorBuilderElementCode, fragment: RenderFragment) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun builder ->
            builder.AddContent(fragment))

    [<Extension>]
    static member inline Yield([<InlineIfLambda>] this: BlazorBuilderElementCode, content: string) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun builder ->
            builder.AddContent(content: string))

    [<Extension>]
    static member inline Yield([<InlineIfLambda>] this: BlazorBuilderElementCode, markup: MarkupString) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun builder ->
            builder.AddMarkupContent(markup.Value))

    [<Extension>]
    static member inline Yield<'import when 'import: struct and 'import :> IComponentImport>([<InlineIfLambda>] this: BlazorBuilderElementCode, _: 'import) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun builder ->
            builder.CloseComponent())

    [<Extension>]
    static member inline Run([<InlineIfLambda>] this: BlazorBuilderElementCode, [<InlineIfLambda>] runExpr: BlazorBuilderElementCode) : BlazorBuilderElementCode =
        BlazorBuilderElementCode(fun builder ->
            this.Invoke(builder)
            runExpr.Invoke(builder))

[<Sealed>]
type BlazorBuilderRunner() =

    inherit RenderTreeBlockBase()
    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderElementCode) =
        fun builder ->
            let builder = BlazorBuilderCore(builder)
            runExpr.Invoke builder

[<Sealed>]
type RenderFragmentRunner() =

    inherit RenderTreeBlockBase()
    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderElementCode) : RenderFragment =
        RenderFragment(fun builder -> runExpr.Invoke (BlazorBuilderCore(builder)))