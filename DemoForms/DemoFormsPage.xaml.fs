namespace DemoForms

open Xamarin.Forms
open Xamarin.Forms.Xaml
open System.Collections.ObjectModel

type DemoViewModel() as this = 

    // A lot of types declared here, are they really needed?
    let collection:ObservableCollection<string> = ObservableCollection<string>()

    let addTodo():unit = collection.Add(this.Entry)

    let addTodo:Command = new Command(addTodo)

    member val Entry:string = "" with get, set
    member this.Todos:ObservableCollection<string> = collection
    member this.AddTodo with get():Command = addTodo


type DemoFormsPage() as this =
    inherit ContentPage()

    let _ = base.LoadFromXaml(typeof<DemoFormsPage>)
    do this.BindingContext <- new DemoViewModel()

