namespace Calculator

open Anything.Shared
open System.ComponentModel.Composition

[<Export(typeof<IPlugin>)>]
type Calculator() =
    interface IPlugin with
        member this.Process input =
            if input.StartsWith("=")
                then Seq.singleton(new CalculatorResult(1u, Result = 5.5) :> IResult) 
                else Seq.empty