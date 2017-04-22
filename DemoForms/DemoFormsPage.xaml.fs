namespace DemoForms

open Xamarin.Forms
open Xamarin.Forms.Xaml
open System.Collections.ObjectModel
open Model

type DemoViewModel() as this = 

    // The type of the colleciton is inferred based on its usage
    let collection = ObservableCollection()

    let markTaskAsCompleted x = 

        // The F# compiler is strict and will throw a warning if you do not use the result of calling a function/method
        // Must be explict about this. Send the result to the ignore function to do this. 
        collection.Remove x |> ignore
        x |> completeTask |> collection.Add

    let addTask () = 
        let task = newTask this.Entry

        // Since DUs and Records implement equality, contains will work as we want it. 
        match collection.Contains task with 
        | false -> collection.Add task
        | true -> ()

    let addTask = Command addTask

    member val Entry = "" with get, set
    member this.Todos = collection
    member this.AddTask with get() = addTask

    // Syntax for overriding setters and getters
    member this.SelectedItem 
        with get() = null
        and set(value) = markTaskAsCompleted <| unbox value


type DemoFormsPage() as this =
    inherit ContentPage()

    let _ = base.LoadFromXaml(typeof<DemoFormsPage>)
    do this.BindingContext <- new DemoViewModel()

