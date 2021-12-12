// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System.Text.Json
open ConsoleAppFramework
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging

[<EntryPoint>]
let main argv =
    let jsonSerializerOptions = JsonSerializerOptions (PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase)
    let options = ConsoleAppOptions(JsonSerializerOptions = jsonSerializerOptions)

    do Host.CreateDefaultBuilder()
           .ConfigureLogging(fun builder -> builder.SetMinimumLevel LogLevel.Trace |> ignore)
           .RunConsoleAppFrameworkAsync(argv, options, null)
       |> Async.AwaitTask
       |> Async.RunSynchronously
    0