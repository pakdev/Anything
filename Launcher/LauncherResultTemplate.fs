namespace Launcher

open Anything.Shared
open System.IO
open System.Xml
open System.Windows
open System.Windows.Markup
open System.ComponentModel.Composition


[<Export(typeof<IPluginTemplate>)>]
type LauncherResultTemplate() = 
    
    interface IPluginTemplate with
        member this.AddToMergedDictionaries() =
            let baseDirectory = __SOURCE_DIRECTORY__
            let xamlPath = Path.Combine([|baseDirectory; "LauncherResultTemplate.xaml"|])
            let reader = XmlReader.Create(xamlPath)
            let dictionary = XamlReader.Load(reader) :?> ResourceDictionary
            Application.Current.Resources.MergedDictionaries.Add(dictionary)