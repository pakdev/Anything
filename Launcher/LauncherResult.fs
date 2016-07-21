namespace Launcher

open Anything.Shared
open System
open System.Diagnostics
open System.Windows
open System.Windows.Input
open System.Windows.Media

type LauncherResult(rank: uint32) as self =

    // Open an explorer window with the file selected
    //startInfo = new ProcessStartInfo
    //{
    //    FileName = $"explorer /n, /select,{result.Path}"
    //};
    //break;
    let launchAction = new Action<_>(fun _ ->
        let startInfo = new ProcessStartInfo(FileName = self.Path)
        let p = new Process(StartInfo = startInfo)
        async {
            try
                p.Start() |> ignore
            with
                | ex -> MessageBox.Show(ex.Message) |> ignore
        } |> Async.Start
    )

    member val Name = "" with get, set
    member val Path = "" with get, set
    member val Icon = null:ImageSource with get, set

    interface IResult with

        member this.Rank with get() = rank
        member this.Launch with get() = new RelayCommand(launchAction) :> ICommand