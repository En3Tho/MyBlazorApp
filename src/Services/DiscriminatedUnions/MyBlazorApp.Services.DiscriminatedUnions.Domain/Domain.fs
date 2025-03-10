﻿namespace MyBlazorApp.Services.DiscriminatedUnions.Domain

open System
open Microsoft.Extensions.Logging
open En3Tho.FSharp.Extensions

type ImportantData =
    | NameAndAge of Name: string * Age: int
    | PriceRangeAndCount of RangeFrom: int * RangeTo: int * Count: int
    | Cart of Items: string[]

module rec DiscriminatedUnions =

    let getRandomImportantData (logger: ILogger) =
        logger.LogTrace("{methodName} called", nameof getRandomImportantData)

        let dt = DateTime.Now
        let rng = Random()

        match rng.Next 3 with
        | 0 -> NameAndAge("Bob", dt.Millisecond)
        | 1 -> PriceRangeAndCount(dt.Minute, dt.Second, dt.Millisecond)
        | _ -> Cart(Array.init 10 ^ fun i -> $"item{i}-{dt.Millisecond}")