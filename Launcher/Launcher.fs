namespace Launcher

open Anything.Shared
open Anything.Everything
open System.Text.RegularExpressions
open System.ComponentModel.Composition

[<Export(typeof<IPlugin>)>]
type Launcher() = 
    let everythingService = new EverythingService()
    let applications = everythingService.Applications

    interface IPlugin with
        member this.Process input =
            // Ranking of matches from best to worst
            // ^input$
            // ^input
            // \binput
            // split into words and search \bword1.+\bword2...
            // input
            // split into characters and search \bi.+\bn.+\bp.+\bu.+\bt
            // split into characters and search \bi.*n.*p.*u.*t

            let (|InputRegex|_|) pattern text name =
                let regex = sprintf pattern text
                let m = Regex(regex, RegexOptions.IgnoreCase).Match(name)
                if m.Success 
                    then Some (List.tail [ for x in m.Groups -> x.Value ])
                    else None

            let getNameRank (name:string) =
                let cleanInput = Regex.Escape(input.Trim())
                let words = Regex.Split(input, @"\s+") |> Array.map Regex.Escape
                let chars = Regex.Split(input, @"\s*") |> Array.map Regex.Escape
                
                match name with
                    | InputRegex "^(%s)$" cleanInput _ -> 1u
                    | InputRegex "^(%s)" cleanInput _ -> 2u
                    | InputRegex @"\b(%s)" cleanInput _ -> 3u
                    | InputRegex @"\b(%s)" (String.concat @").+\b(" words) _ when words.Length >= 2 -> 4u
                    | InputRegex @"\b(%s)" (String.concat @").+\b(" chars) _ when words.Length = 1 && chars.Length <= 5 -> 5u
                    | InputRegex @"\b(%s)" (String.concat @").*(" chars) _ -> 6u
                    | _ -> 10u

            let getLauncherResult (app:SearchResult) =
                let rank = getNameRank app.Name
                new LauncherResult(rank, Name=app.Name, Path=app.Path, Icon=null) :> IResult

            let ranks = 
                applications
                |> Seq.map getLauncherResult
                |> Seq.sortBy (fun x -> x.Rank)
                |> Seq.take 5

            ranks
