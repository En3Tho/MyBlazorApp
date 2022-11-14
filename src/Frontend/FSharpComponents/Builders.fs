namespace rec En3Tho.FSharp.ComputationExpressions.BlazorBuilder

open System
open System.Collections.Generic
open System.Runtime.CompilerServices
open En3Tho.FSharp.ComputationExpressions.BlazorBuilder.Core
open Microsoft.AspNetCore.Components

// TODO: use generics to define code
type BlazorBuilderAttributeCode = BlazorBuilderCore -> unit -> unit // erm
type ComponentImportCode<'a when 'a: struct and 'a :> IComponentImport> = BlazorBuilderCore -> 'a
type BlazorBuilderCode2 = delegate of BlazorBuilderCore -> unit

type BlazorBuilderBase() =
    member inline this.While([<InlineIfLambda>] moveNext: unit -> bool, [<InlineIfLambda>] whileExpr: BlazorBuilderCode2) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder -> while moveNext() do (whileExpr.Invoke(builder)))

    member inline this.Combine([<InlineIfLambda>] first: BlazorBuilderCode2, [<InlineIfLambda>] second: BlazorBuilderCode2) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    member inline this.TryFinally([<InlineIfLambda>] tryExpr: BlazorBuilderCode2, [<InlineIfLambda>] compensation: BlazorBuilderCode2) =
        BlazorBuilderCode2(fun (builder) ->
            try
                tryExpr.Invoke(builder)
            finally
                compensation.Invoke(builder))

    member inline this.TryWith([<InlineIfLambda>] tryExpr, [<InlineIfLambda>] compensation: exn -> BlazorBuilderCode2) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun (builder) ->
            try
                tryExpr()
            with e ->
                (compensation e).Invoke(builder))

    member inline this.Using(resource: #IDisposable, [<InlineIfLambda>] tryExpr: #IDisposable -> BlazorBuilderCode2) : BlazorBuilderCode2 =
        this.TryFinally(
            (fun (builder) -> (tryExpr(resource).Invoke(builder))),
            (fun (builder) -> if not (isNull (box resource)) then resource.Dispose()))

    member inline this.For(values: 'a seq, [<InlineIfLambda>] forExpr: 'a -> BlazorBuilderCode2) : BlazorBuilderCode2 =
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
    member inline _.Zero() : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder ->
            builder.Advance())

    member inline this.Delay([<InlineIfLambda>] delay: unit -> BlazorBuilderCode2) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun (builder) -> (delay()).Invoke(builder))

type RenderTreeBlockBase() =
    inherit BlazorBuilderBase()

    member inline _.Yield([<InlineIfLambda>] codeBuilderCode: BlazorBuilderCode2) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder ->
            codeBuilderCode.Invoke builder
            builder.CloseElement())

    member inline _.Yield([<InlineIfLambda>] attributeCode: BlazorBuilderAttributeCode) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder ->
            attributeCode builder ())

    member inline _.Yield([<InlineIfLambda>] codeBuilderCode: ComponentImportCode<_>) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder ->
            codeBuilderCode builder |> ignore
            builder.CloseComponent())

    member inline _.Yield(fragment: RenderFragment) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder ->
            builder.AddContent(fragment))

    member inline _.Yield(content: string) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder ->
            builder.AddContent(content: string))

    member inline _.Yield(markup: MarkupString) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder ->
            builder.AddMarkupContent(markup.Value))

    member inline _.Yield(_: ComponentBlock<'a>) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder ->
            builder.OpenComponent<'a>()
            builder.CloseComponent())

[<Sealed>]
type AttributeBlock() =
    inherit BlazorBuilderBase()

    member inline _.Yield<'attr when 'attr: struct and 'attr :> IAttribute>(attr: 'attr) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder ->
            attr.RenderTo builder)

    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderCode2) : BlazorBuilderAttributeCode =
        fun builder _ ->
            runExpr.Invoke builder

[<Sealed>]
type ComponentBlock<'a when 'a :> ComponentBase>() =
    inherit BlazorBuilderBase()

    member inline _.Yield<'attr when 'attr: struct and 'attr :> IAttribute>(attr: 'attr) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder ->
            attr.RenderTo builder)

    static member val Instance = ComponentBlock<'a>()

    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderCode2) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder ->
            builder.OpenComponent<'a>()
            runExpr.Invoke builder
            builder.CloseComponent())

type ElementBlockBase<'name when 'name :> IElementName and 'name: struct>() =
    inherit BlazorBuilderBase()
    member _.Name = Unchecked.defaultof<'name>.Name

    member inline this.Yield<'attr, 'import when 'attr: struct and 'attr :> IAttribute and 'import :> IComponentImport>(_: 'import, attr: 'attr) =
        fun builder ->
            attr.RenderTo builder

    member inline _.Yield<'attr when 'attr: struct and 'attr :> IAttribute>(attr: 'attr) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder ->
            attr.RenderTo builder)

    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderCode2) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder ->
            builder.OpenElement(this.Name)
            runExpr.Invoke builder)

[<AbstractClass; Sealed; Extension>]
type ComponentImportBlock() =

    [<Extension>]
    static member inline While(_: #IComponentImport, [<InlineIfLambda>] moveNext: unit -> bool, [<InlineIfLambda>] whileExpr: BlazorBuilderCode2) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder -> while moveNext() do (whileExpr.Invoke(builder)))

    [<Extension>]
    static member inline Combine(_: #IComponentImport, [<InlineIfLambda>] first: BlazorBuilderCode2, [<InlineIfLambda>] second: BlazorBuilderCode2) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    [<Extension>]
    static member inline TryFinally(_: #IComponentImport, [<InlineIfLambda>] tryExpr: BlazorBuilderCode2, [<InlineIfLambda>] compensation: BlazorBuilderCode2) =
        BlazorBuilderCode2(fun (builder) ->
            try
                tryExpr.Invoke(builder)
            finally
                compensation.Invoke(builder))

    [<Extension>]
    static member inline TryWith(_: #IComponentImport, [<InlineIfLambda>] tryExpr, [<InlineIfLambda>] compensation: exn -> BlazorBuilderCode2) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun (builder) ->
            try
                tryExpr()
            with e ->
                (compensation e).Invoke(builder))

    [<Extension>]
    static member inline Using(this: #IComponentImport, resource: #IDisposable, [<InlineIfLambda>] tryExpr: #IDisposable -> BlazorBuilderCode2) : BlazorBuilderCode2 =
        this.TryFinally(
            (fun (builder) -> (tryExpr(resource).Invoke(builder))),
            (fun (builder) -> if not (isNull (box resource)) then resource.Dispose()))

    [<Extension>]
    static member inline For(this: #IComponentImport, values: 'a seq, [<InlineIfLambda>] forExpr: 'a -> BlazorBuilderCode2) : BlazorBuilderCode2 =
        this.Using(
            values.GetEnumerator(), (fun (e: IEnumerator<'a>) ->
                this.While((fun () -> e.MoveNext()), (fun (builder) ->
                    (forExpr e.Current).Invoke(builder))
                )
            )
        )

    [<Extension>]
    static member inline Zero(_: #IComponentImport) : BlazorBuilderCode2 = BlazorBuilderCode2(fun (builder) -> builder.Advance())

    [<Extension>]
    static member inline Delay(_: #IComponentImport, [<InlineIfLambda>] delay: unit -> BlazorBuilderCode2) =
        fun (builder) -> (delay()).Invoke(builder)

    [<Extension>]
    static member inline Yield<'attr, 'import when 'attr: struct and 'attr :> IAttribute and 'import :> IComponentImport>(_: 'import, attr: 'attr) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder ->
            attr.RenderTo builder)

    [<Extension>]
    static member inline Run(this: #IComponentImport, [<InlineIfLambda>] runExpr: BlazorBuilderCode2) =
        runExpr.Invoke this.Builder
        this

[<AbstractClass; Sealed; Extension>]
type BlazorBuilderCodeBlock() =

    [<Extension>]
    static member inline While([<InlineIfLambda>] this: BlazorBuilderCode2, [<InlineIfLambda>] moveNext: unit -> bool, [<InlineIfLambda>] whileExpr: BlazorBuilderCode2) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder -> while moveNext() do (whileExpr.Invoke(builder)))

    [<Extension>]
    static member inline Combine([<InlineIfLambda>] this: BlazorBuilderCode2, [<InlineIfLambda>] first: BlazorBuilderCode2, [<InlineIfLambda>] second: BlazorBuilderCode2) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    [<Extension>]
    static member inline TryFinally([<InlineIfLambda>] this: BlazorBuilderCode2, [<InlineIfLambda>] tryExpr: BlazorBuilderCode2, [<InlineIfLambda>] compensation: BlazorBuilderCode2) =
        BlazorBuilderCode2(fun (builder) ->
            try
                tryExpr.Invoke(builder)
            finally
                compensation.Invoke(builder))

    [<Extension>]
    static member inline TryWith([<InlineIfLambda>] this: BlazorBuilderCode2, [<InlineIfLambda>] tryExpr, [<InlineIfLambda>] compensation: exn -> BlazorBuilderCode2) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun (builder) ->
            try
                tryExpr()
            with e ->
                (compensation e).Invoke(builder))

    [<Extension>]
    static member inline Using([<InlineIfLambda>] this: BlazorBuilderCode2, resource: #IDisposable, [<InlineIfLambda>] tryExpr: #IDisposable -> BlazorBuilderCode2) : BlazorBuilderCode2 =
        this.TryFinally(
            (fun (builder) -> (tryExpr(resource).Invoke(builder))),
            (fun (builder) -> if not (isNull (box resource)) then resource.Dispose()))

    [<Extension>]
    static member inline For([<InlineIfLambda>] this: BlazorBuilderCode2, values: 'a seq, [<InlineIfLambda>] forExpr: 'a -> BlazorBuilderCode2) : BlazorBuilderCode2 =
        this.Using(
            values.GetEnumerator(), (fun (e: IEnumerator<'a>) ->
                this.While((fun () -> e.MoveNext()), (fun (builder) ->
                    (forExpr e.Current).Invoke(builder))
                )
            )
        )

    [<Extension>]
    static member inline Zero([<InlineIfLambda>] this: BlazorBuilderCode2) : BlazorBuilderCode2 = BlazorBuilderCode2(fun (builder) -> builder.Advance())

    [<Extension>]
    static member inline Delay([<InlineIfLambda>] this: BlazorBuilderCode2, [<InlineIfLambda>] delay: unit -> BlazorBuilderCode2) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun (builder) -> (delay()).Invoke(builder))
    
    [<Extension>]
    static member inline Yield([<InlineIfLambda>] this: BlazorBuilderCode2, [<InlineIfLambda>] codeBuilderCode: BlazorBuilderCode2) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder ->
            codeBuilderCode.Invoke builder)

    [<Extension>]
    static member inline Yield([<InlineIfLambda>] this: BlazorBuilderCode2, [<InlineIfLambda>] attributeCode: BlazorBuilderAttributeCode) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder ->
            attributeCode builder ())

    [<Extension>]
    static member inline Yield([<InlineIfLambda>] this: BlazorBuilderCode2, [<InlineIfLambda>] codeBuilderCode: ComponentImportCode<_>) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder ->
            codeBuilderCode builder |> ignore
            builder.CloseComponent())

    [<Extension>]
    static member inline Yield([<InlineIfLambda>] this: BlazorBuilderCode2, fragment: RenderFragment) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder ->
            builder.AddContent(fragment))

    [<Extension>]
    static member inline Yield([<InlineIfLambda>] this: BlazorBuilderCode2, content: string) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder ->
            builder.AddContent(content: string))

    [<Extension>]
    static member inline Yield([<InlineIfLambda>] this: BlazorBuilderCode2, markup: MarkupString) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder ->
            builder.AddMarkupContent(markup.Value))

    [<Extension>]
    static member inline Yield<'import when 'import: struct and 'import :> IComponentImport>([<InlineIfLambda>] this: BlazorBuilderCode2, _: 'import) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder ->
            builder.CloseComponent())

    [<Extension>]
    static member inline Run([<InlineIfLambda>] this: BlazorBuilderCode2, [<InlineIfLambda>] runExpr: BlazorBuilderCode2) : BlazorBuilderCode2 =
        BlazorBuilderCode2(fun builder ->
            this.Invoke(builder)
            runExpr.Invoke(builder))

[<Sealed>]
type BlazorBuilderRunner() =

    inherit RenderTreeBlockBase()
    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderCode2) =
        fun builder ->
            let builder = BlazorBuilderCore(builder)
            runExpr.Invoke builder

[<Sealed>]
type RenderFragmentRunner() =

    inherit RenderTreeBlockBase()
    member inline this.Run([<InlineIfLambda>] runExpr: BlazorBuilderCode2) : RenderFragment =
        RenderFragment(fun builder -> runExpr.Invoke (BlazorBuilderCore(builder)))