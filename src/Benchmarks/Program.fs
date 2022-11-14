// For more information see https://aka.ms/fsharp-console-apps

open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running
open Microsoft.AspNetCore.Components.Rendering
open FSharpComponents
open MyBlazorApp.ComponentsAndPages.Components

// |   Method |     Mean |   Error |  StdDev | Code Size |   Gen0 | Allocated |
// |--------- |---------:|--------:|--------:|----------:|-------:|----------:|
// | RenderFS | 264.7 ns | 1.24 ns | 1.10 ns |     248 B | 0.0315 |     528 B |
// | RenderCS | 217.4 ns | 1.14 ns | 1.01 ns |   1,951 B | 0.0243 |     408 B |

[<MemoryDiagnoser; DisassemblyDiagnoser(filters = [||])>]
type RenderTreeBuilderBenchmark() =

    let builder = RenderTreeBuilder()

    [<Benchmark>]
    member _.RenderFS() =
        builder.Clear()
        CounterFSharp.Test(builder)

    [<Benchmark>]
    member _.RenderCS() =
        builder.Clear()
        CounterCSharp.Test(builder)

BenchmarkRunner.Run<RenderTreeBuilderBenchmark>() |> ignore