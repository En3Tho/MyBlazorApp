namespace rec En3Tho.FSharp.BlazorBuilder

open System
open System.Collections.Generic
open En3Tho.FSharp.BlazorBuilder.Core
open Microsoft.AspNetCore.Components

type BlazorBuilderBase() =

    member inline this.Combine([<InlineIfLambda>] first: BlazorBuilderCode, [<InlineIfLambda>] second: BlazorBuilderCode) : BlazorBuilderCode =
        BlazorBuilderCode(fun(builder) ->
            first.Invoke(builder)
            second.Invoke(builder))

    // This is needed to preserve correct sequence counts in RenderTreeBuilder in case of non-yield operations (eg if ... then ...)
    // I'm not sure how does this work with while or for
    // maybe this isn't really needed? Need to check out diff mechanism
    member inline _.Zero() : BlazorBuilderCode = BlazorBuilderCode(fun builder -> builder.Advance())

    member inline this.Delay([<InlineIfLambda>] delay: unit -> BlazorBuilderCode) : BlazorBuilderCode =
        BlazorBuilderCode(fun (builder) -> (delay()).Invoke(builder))

type BlazorRunnerBase() =
    inherit BlazorBuilderBase()

    member inline this.While([<InlineIfLambda>] moveNext: unit -> bool, [<InlineIfLambda>] whileExpr: BlazorBuilderCode) : BlazorBuilderCode =
        BlazorBuilderCode(fun builder ->
            let sequence = builder.GetSequenceCountForWhileExpression()
            while moveNext() do
                builder.ResumeSequenceAtStartOfWhileExpression(sequence)
                (whileExpr.Invoke(builder)))

    member inline this.TryFinally([<InlineIfLambda>] tryExpr: BlazorBuilderCode, [<InlineIfLambda>] compensation: BlazorBuilderCode) =
        BlazorBuilderCode(fun (builder) ->
            try
                tryExpr.Invoke(builder)
            finally
                compensation.Invoke(builder))

    member inline this.TryWith([<InlineIfLambda>] tryExpr, [<InlineIfLambda>] compensation: exn -> BlazorBuilderCode) : BlazorBuilderCode =
        BlazorBuilderCode(fun (builder) ->
            try
                tryExpr()
            with e ->
                (compensation e).Invoke(builder))

    member inline this.Using(resource: #IDisposable, [<InlineIfLambda>] tryExpr: #IDisposable -> BlazorBuilderCode) : BlazorBuilderCode =
        this.TryFinally(
            (fun (builder) -> (tryExpr(resource).Invoke(builder))),
            (fun (builder) -> if not (isNull (box resource)) then resource.Dispose()))

    member inline this.For(values: 'a seq, [<InlineIfLambda>] forExpr: 'a -> BlazorBuilderCode) : BlazorBuilderCode =
        this.Using(
            values.GetEnumerator(), (fun (e: IEnumerator<'a>) ->
                this.While((fun () -> e.MoveNext()), (fun (builder) ->
                    (forExpr e.Current).Invoke(builder))
                )
            )
        )

type RenderTreeBlockBase() =
    inherit BlazorRunnerBase()

    member inline _.Yield([<InlineIfLambda>] codeBuilderCode: BlazorBuilderCode) : BlazorBuilderCode =
        BlazorBuilderCode(fun builder ->
            codeBuilderCode.Invoke builder
            builder.CloseElement())

    member inline _.Yield([<InlineIfLambda>] codeBuilderCode: BlazorBuilderComponentCode) : BlazorBuilderCode =
        BlazorBuilderCode(fun builder ->
            codeBuilderCode builder ())

    member inline _.Yield([<InlineIfLambda>] codeBuilderCode: ComponentImportCode<_>) : BlazorBuilderCode =
        BlazorBuilderCode(fun builder ->
            codeBuilderCode builder |> ignore
            builder.CloseComponent())

    member inline _.Yield(fragment: RenderFragment) : BlazorBuilderCode =
        BlazorBuilderCode(fun builder ->
            builder.AddContent(fragment))

    member inline _.Yield(content: string) : BlazorBuilderCode =
        BlazorBuilderCode(fun builder ->
            builder.AddContent(content: string))

    member inline _.Yield(markup: MarkupString) : BlazorBuilderCode =
        BlazorBuilderCode(fun builder ->
            builder.AddMarkupContent(markup.Value))