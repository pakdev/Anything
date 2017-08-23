namespace Calculator

open Anything.Shared
open System.IO
open System.Xml
open System.Windows
open System.Windows.Markup
open System.ComponentModel.Composition

[<Export(typeof<IPluginTemplate>)>]
type CalculatorResultTemplate() =

    interface IPluginTemplate with
        member this.AddToMergedDictionaries() =
            let baseDirectory = __SOURCE_DIRECTORY__
            let xamlPath = Path.Combine([|baseDirectory; "CalculatorResultTemplate.xaml"|])
            let reader = XmlReader.Create(xamlPath)
            let dictionary = XamlReader.Load(reader) :?> ResourceDictionary
            Application.Current.Resources.MergedDictionaries.Add(dictionary)
    

