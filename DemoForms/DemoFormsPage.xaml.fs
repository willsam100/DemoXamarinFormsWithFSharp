namespace DemoForms

open System
open System.Diagnostics
open Xamarin.Forms
open Xamarin.Forms.Xaml
open System.Collections.ObjectModel

open Model

type TaskViewModel() as this = 

    let collection = ObservableCollection()

    let markTaskAsCompleted x = 
        collection.Remove (Task x) |> ignore
        x |> completeTask |> Task |> collection.Add

    let addTask () = 
        let task = Task (newTask this.Entry)
        match collection.Contains task with 
        | false -> collection.Add task
        | true -> ()

    let addNote () = 
        let note = Note this.Entry
        match collection.Contains note with 
        | false -> collection.Add note
        | true -> ()

    member val Entry = "" with get, set
    member this.Todos = collection
    member this.AddTask 
        with get(): Command = new Command(addTask)

    member this.AddNote 
        with get(): Command = new Command(addNote)

    member this.SelectedItem 
        with get() = null
        and set(value) = 
            match unbox value with 
            | Task x -> markTaskAsCompleted x
            | Note _ -> ()



type DemoViewModel() = 

    let collection: ObservableCollection<string> = new ObservableCollection<string>()

    member val Entry = "" with get, set

    member this.Todos: ObservableCollection<string> = collection

    member this.AddItem(): unit  = collection.Add(this.Entry)

    member this.Clicked 
        with get(): Command = new Command(this.AddItem)


type DemoFormsPage() as this =
    inherit ContentPage()

    let _ = base.LoadFromXaml(typeof<DemoFormsPage>)
    do this.BindingContext <- new TaskViewModel()

