namespace MyBlazorApp.Utility.Logging.CSharp

open System
open System.Runtime.CompilerServices
open Microsoft.Extensions.Logging

[<Struct>]
type Logger(logger: ILogger, logLevel: LogLevel) = // should this be like ILogger?
    member _.Log(message, [<ParamArray>] args) =
        logger.Log(logLevel, message, args)

    member _.Log(exn: Exception, message, [<ParamArray>] args) =
        logger.Log(logLevel, exn, message, args)

[<Extension>]
[<AbstractClass; Sealed>]
type ILoggerExtensions() =
    [<Extension>]
    static member Debug(logger: ILogger) =
        if logger.IsEnabled LogLevel.Debug then Nullable(Logger(logger, LogLevel.Debug)) else Nullable()

    [<Extension>]
    static member Error(logger: ILogger) =
        if logger.IsEnabled LogLevel.Error then Nullable(Logger(logger, LogLevel.Error)) else Nullable()

    [<Extension>]
    static member Information(logger: ILogger) =
        if logger.IsEnabled LogLevel.Information then Nullable(Logger(logger, LogLevel.Information)) else Nullable()

    [<Extension>]
    static member None(logger: ILogger) =
        if logger.IsEnabled LogLevel.None then Nullable(Logger(logger, LogLevel.None)) else Nullable()

    [<Extension>]
    static member Trace(logger: ILogger) =
        if logger.IsEnabled LogLevel.Trace then Nullable(Logger(logger, LogLevel.Trace)) else Nullable()

    [<Extension>]
    static member Warning(logger: ILogger) =
        if logger.IsEnabled LogLevel.Warning then Nullable(Logger(logger, LogLevel.Warning)) else Nullable()