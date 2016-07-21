namespace Calculator

open Anything.Shared
open System
open System.Windows.Input

type CalculatorResult(rank: uint32) =
    let launchAction = new Action<_>(fun _ -> ())

    member val Result = 0.0 with get, set

    interface IResult with

        member this.Rank with get() = rank
        member this.Launch with get() = new RelayCommand(launchAction) :> ICommand