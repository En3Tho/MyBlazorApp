namespace rec En3Tho.FSharp.BlazorBuilder

open System
open System.Collections.Generic
open En3Tho.FSharp.BlazorBuilder.Core
open Microsoft.AspNetCore.Components

type BlazorElementOrComponentBuilderBase() =
    member inline this.Combine([<InlineIfLambda>] first: BlazorBuilderMarkupCode, [<InlineIfLambda>] second: BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    member inline this.Combine([<InlineIfLambda>] first: BlazorBuilderZeroCode, [<InlineIfLambda>] second: BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    member inline this.Combine([<InlineIfLambda>] first: BlazorBuilderElementCode, [<InlineIfLambda>] second: BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    member inline this.Combine([<InlineIfLambda>] first: BlazorBuilderComponentCode, [<InlineIfLambda>] second: BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    member inline this.Combine([<InlineIfLambda>] first: BlazorBuilderAttributeCode, [<InlineIfLambda>] second: BlazorBuilderAttributeCode) : BlazorBuilderAttributeCode =
        BlazorBuilderAttributeCode(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    member inline this.Combine([<InlineIfLambda>] first: BlazorBuilderZeroCode, [<InlineIfLambda>] second: BlazorBuilderAttributeCode) : BlazorBuilderAttributeCode =
        BlazorBuilderAttributeCode(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    // This is needed to preserve correct sequence counts in RenderTreeBuilder in case of non-yield operations (eg if ... then ...)
    // I'm not sure how does this work with while or for
    // maybe this isn't really needed? Need to check out diff mechanism
    member inline _.Zero() : BlazorBuilderZeroCode = BlazorBuilderZeroCode(fun builder -> builder.Advance())

    member inline this.Delay([<InlineIfLambda>] delay: unit -> BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun (builder) -> (delay()).Invoke(builder))

    member inline this.Delay([<InlineIfLambda>] delay: unit -> BlazorBuilderElementCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun (builder) -> (delay()).Invoke(builder))

    member inline this.Delay([<InlineIfLambda>] delay: unit -> BlazorBuilderComponentCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun (builder) -> (delay()).Invoke(builder))

    member inline this.Delay([<InlineIfLambda>] delay: unit -> BlazorBuilderAttributeCode) : BlazorBuilderAttributeCode =
        BlazorBuilderAttributeCode(fun (builder) -> (delay()).Invoke(builder))

    member inline this.Delay([<InlineIfLambda>] delay: unit -> BlazorBuilderZeroCode) : BlazorBuilderZeroCode =
        BlazorBuilderZeroCode(fun (builder) -> (delay()).Invoke(builder))


    member inline _.Yield<'attr when 'attr: struct and 'attr :> IAttribute>(attr: 'attr) : BlazorBuilderAttributeCode =
        BlazorBuilderAttributeCode(fun builder ->
            attr.RenderTo builder)


    member inline this.While([<InlineIfLambda>] moveNext: unit -> bool, [<InlineIfLambda>] whileExpr: BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            let sequence = builder.GetSequenceCountForWhileExpression()
            while moveNext() do
                builder.ResumeSequenceAtStartOfWhileExpression(sequence)
                (whileExpr.Invoke(builder)))

    member inline this.TryFinally([<InlineIfLambda>] tryExpr: BlazorBuilderMarkupCode, [<InlineIfLambda>] compensation: BlazorBuilderMarkupCode) =
        BlazorBuilderMarkupCode(fun (builder) ->
            try
                tryExpr.Invoke(builder)
            finally
                compensation.Invoke(builder))

    member inline this.TryWith([<InlineIfLambda>] tryExpr, [<InlineIfLambda>] compensation: exn -> BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun (builder) ->
            try
                tryExpr()
            with e ->
                (compensation e).Invoke(builder))

    member inline this.Using(resource: #IDisposable, [<InlineIfLambda>] tryExpr: #IDisposable -> BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        this.TryFinally(
            (fun (builder) -> (tryExpr(resource).Invoke(builder))),
            (fun (builder) -> if not (isNull (box resource)) then resource.Dispose()))

    member inline this.For(values: 'a seq, [<InlineIfLambda>] forExpr: 'a -> BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        this.Using(
            values.GetEnumerator(), (fun (e: IEnumerator<'a>) ->
                this.While((fun () -> e.MoveNext()), (fun (builder) ->
                    (forExpr e.Current).Invoke(builder))
                )
            )
        )

type BlazorBuilderGenericMarkupBase() =

    member inline this.Combine([<InlineIfLambda>] first: BlazorBuilderMarkupCode, [<InlineIfLambda>] second: BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    member inline _.Zero() : BlazorBuilderMarkupCode = BlazorBuilderMarkupCode(fun builder -> builder.Advance())

    member inline this.Delay([<InlineIfLambda>] delay: unit -> BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun (builder) -> (delay()).Invoke(builder))

type BlazorRunnerBase() =
    inherit BlazorBuilderGenericMarkupBase()

    member inline this.While([<InlineIfLambda>] moveNext: unit -> bool, [<InlineIfLambda>] whileExpr: BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            let sequence = builder.GetSequenceCountForWhileExpression()
            while moveNext() do
                builder.ResumeSequenceAtStartOfWhileExpression(sequence)
                (whileExpr.Invoke(builder)))

    member inline this.TryFinally([<InlineIfLambda>] tryExpr: BlazorBuilderMarkupCode, [<InlineIfLambda>] compensation: BlazorBuilderMarkupCode) =
        BlazorBuilderMarkupCode(fun (builder) ->
            try
                tryExpr.Invoke(builder)
            finally
                compensation.Invoke(builder))

    member inline this.TryWith([<InlineIfLambda>] tryExpr, [<InlineIfLambda>] compensation: exn -> BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun (builder) ->
            try
                tryExpr()
            with e ->
                (compensation e).Invoke(builder))

    member inline this.Using(resource: #IDisposable, [<InlineIfLambda>] tryExpr: #IDisposable -> BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        this.TryFinally(
            (fun (builder) -> (tryExpr(resource).Invoke(builder))),
            (fun (builder) -> if not (isNull (box resource)) then resource.Dispose()))

    member inline this.For(values: 'a seq, [<InlineIfLambda>] forExpr: 'a -> BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        this.Using(
            values.GetEnumerator(), (fun (e: IEnumerator<'a>) ->
                this.While((fun () -> e.MoveNext()), (fun (builder) ->
                    (forExpr e.Current).Invoke(builder))
                )
            )
        )

type RenderTreeBlockBase() =
    inherit BlazorRunnerBase()

    member inline _.Yield([<InlineIfLambda>] codeBuilderCode: BlazorBuilderMarkupCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            codeBuilderCode.Invoke builder
            builder.CloseElement())

    member inline _.Yield([<InlineIfLambda>] codeBuilderCode: BlazorBuilderChildContentCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            codeBuilderCode.Invoke builder
            builder.CloseComponent())

    member inline _.Yield([<InlineIfLambda>] codeBuilderCode: BlazorBuilderComponentCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            codeBuilderCode.Invoke builder)

    member inline _.Yield([<InlineIfLambda>] codeBuilderCode: BlazorBuilderElementCode) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            codeBuilderCode.Invoke builder)

    member inline _.Yield([<InlineIfLambda>] codeBuilderCode: ComponentImportCode<_>) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            codeBuilderCode builder |> ignore
            builder.CloseComponent())

    member inline _.Yield(fragment: RenderFragment) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            builder.AddContent(fragment))

    member inline _.Yield(content: string) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            builder.AddContent(content: string))

    member inline _.Yield(markup: MarkupString) : BlazorBuilderMarkupCode =
        BlazorBuilderMarkupCode(fun builder ->
            builder.AddMarkupContent(markup.Value))