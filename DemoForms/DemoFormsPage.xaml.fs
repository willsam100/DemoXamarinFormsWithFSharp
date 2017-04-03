namespace DemoForms

open Xamarin.Forms
open Xamarin.Forms.Xaml
open System.Collections.ObjectModel

type ViewModel() = 

    let collection = new ObservableCollection<string>()

    member val Entry = "" with get, set

    member this.AddItem(item: string): unit = collection.Add item
    member this.Items: ObservableCollection<string> = collection


type DemoFormsPage() as this =
    inherit ContentPage()

    let _ = base.LoadFromXaml(typeof<DemoFormsPage>)
    do this.BindingContext <- new ViewModel()






