namespace MyBlazorApp.Utility.Logging

open System
open Microsoft.Extensions.Logging
open En3Tho.FSharp.Extensions

type SimpleConsoleLogger(_logLevel: LogLevel, categoryName: string) =
    interface ILogger with
        member this.BeginScope(state) = { new IDisposable with member _.Dispose() = () }
        member this.IsEnabled(logLevel) = logLevel >= _logLevel
        member this.Log(logLevel, eventId, state, ``exception``, formatter) =
            let message = categoryName + ": " + formatter.Invoke(state, ``exception``)
            Console.WriteLine message

type SimpleConsoleLoggerProvider(logLevel: LogLevel) =
    interface ILoggerProvider with
        member this.CreateLogger(categoryName) = SimpleConsoleLogger(logLevel, categoryName) :> ILogger
        member this.Dispose() = ()

module ILoggerExtensions =
    type ILogger with
        member inline this.Criticalf format = Printf.cksprintf (this.IsEnabled LogLevel.Critical) this.LogCritical format
        member inline this.Errorf format = Printf.cksprintf (this.IsEnabled LogLevel.Error) this.LogError format
        member inline this.Warningf format = Printf.cksprintf (this.IsEnabled LogLevel.Warning) this.LogWarning format
        member inline this.Informationf format = Printf.cksprintf (this.IsEnabled LogLevel.Information) this.LogInformation format
        member inline this.Debugf format = Printf.cksprintf (this.IsEnabled LogLevel.Debug) this.LogDebug format
        member inline this.Tracef format = Printf.cksprintf (this.IsEnabled LogLevel.Trace) this.LogTrace format
        member inline this.Nonef format = Printf.cksprintf false this.LogDebug format