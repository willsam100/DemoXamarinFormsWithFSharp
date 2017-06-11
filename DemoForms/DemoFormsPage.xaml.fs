namespace DemoForms

open Xamarin.Forms
open Xamarin.Forms.Xaml
open System.Collections.ObjectModel
open Model

type DemoViewModel() as this = 

    // The type of the colleciton is inferred based on its usage
    let collection = ObservableCollection()

    let markTodoAsCompleted x = 

        // The F# compiler is strict and will throw a warning if you do not use the result of calling a function/method
        // Must be explict about this. Send the result to the ignore function to do this. 
        collection.Remove x |> ignore
        x |> completeTodo |> collection.Add

    let addTodo () = 
        let todo = newTodo this.Entry

        // Since DUs and Records implement equality, contains will work as we want it. 
        match collection.Contains todo with 
        | false -> collection.Add todo
        | true -> ()

    let addTodo = Command addTodo

    member val Entry = "" with get, set
    member this.Todos = collection
    member this.AddTodo with get() = addTodo

    // Syntax for overriding setters and getters
    member this.SelectedItem 
        with get() = null
        and set(value) = markTodoAsCompleted <| unbox value


type DemoFormsPage() as this =
    inherit ContentPage()

    let _ = base.LoadFromXaml(typeof<DemoFormsPage>)
    do this.BindingContext <- new DemoViewModel()

