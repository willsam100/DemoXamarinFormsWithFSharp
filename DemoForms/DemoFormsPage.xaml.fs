namespace DemoForms

open Xamarin.Forms
open Xamarin.Forms.Xaml
open System.Collections.ObjectModel
open Model

type DemoViewModel() as this = 

    // The type of the colleciton is inferred based on its usage
    let collection = ObservableCollection()

    let markTaskAsCompleted x = 
        match x with 
        | Task todo -> x |> collection.Remove |> ignore
                       todo |> completeTask |> Task |> collection.Add
        | Note _ -> ()

    let addTask () =  
        newTask this.Entry |> Task |> collection.Add

    let addNote () =   
        Note this.Entry |> collection.Add


    let addTask = Command addTask
    let addNote = Command addNote

    member val Entry = "" with get, set
    member this.Todos = collection
    member this.AddTask with get() = addTask
    member this.AddNote with get() = addNote

    // Syntax for overriding setters and getters
    member this.SelectedItem 
        with get() = null
        and set(value) = 
            markTaskAsCompleted <| unbox value

type DemoFormsPage() as this =
    inherit ContentPage()

    let _ = base.LoadFromXaml(typeof<DemoFormsPage>)
    do this.BindingContext <- new DemoViewModel()

