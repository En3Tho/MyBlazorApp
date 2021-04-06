namespace MyBlazorApp.Domain.Types

[<Struct>]
type PositiveIntError = ValueIsLessThanZero of int
    with
        member this.Value = let (ValueIsLessThanZero value) = this in value
        member this.GetErrorMessage() = $"Value is less than zero: {this.Value}"

[<Struct>]
type PositiveInt = private PositiveInt of int
    with
        static member TryCreate value =
            if value < 0 then ValueIsLessThanZero value |> Error
            else PositiveInt value |> Ok
        static member GetValue (PositiveInt value) = value
        member this.Value = let (PositiveInt value) = this in value