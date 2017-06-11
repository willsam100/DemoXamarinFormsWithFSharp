namespace DemoForms

open Xamarin.Forms
open Xamarin.Forms.Xaml
open System.Collections.ObjectModel
open Model

type DemoViewModel() as this = 

    // The type of the colleciton is inferred based on its usage
    let collection = ObservableCollection()

    let markTodoAsCompleted x = 
        match x with 
        | Todo todo -> x |> collection.Remove |> ignore
                       todo |> completeTodo |> Todo |> collection.Add
        | Note _ -> ()

    let addTodo () =  
        newTodo this.Entry |> Todo |> collection.Add

    let addNote () =   
        Note this.Entry |> collection.Add


    let addTodo = Command addTodo
    let addNote = Command addNote

    member val Entry = "" with get, set
    member this.Todos = collection
    member this.AddTodo with get() = addTodo
    member this.AddNote with get() = addNote

    // Syntax for overriding setters and getters
    member this.SelectedItem 
        with get() = null
        and set(value) = 
            markTodoAsCompleted(unbox(value))

type DemoFormsPage() as this =
    inherit ContentPage()

    let _ = base.LoadFromXaml(typeof<DemoFormsPage>)
    do this.BindingContext <- new DemoViewModel()

