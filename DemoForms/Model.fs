module DemoForms.Model
open System

type TodoItem = {
    Task: string 
    DueDate: DateTime
    IsCompleted: bool
}

let newTask task dueDate =
    {Task = task; DueDate = dueDate; IsCompleted = false}

let completeTask task = 
    {task with IsCompleted = true}

let findNextTask (tasks: TodoItem list) = 
    match tasks with 
    [] -> None
    | first::rest -> 
        tasks 
        |> List.toSeq
        |> Seq.filter (fun x -> not x.IsCompleted)
        |> Seq.minBy (fun x -> x.DueDate)
        |> Some

type Animal = 
    | Dog
    | Cat

let speak a = 
    match a with 
    | Dog -> "Bark, Bark"
    | Cat -> "Meow, Meow" 

let toAnimal a = 
    match a with 
    | "dog" -> Dog
    | "cat" -> Cat
    | _ -> Dog

let toString a = 
    match a with 
    | Dog -> "dog"
    | Cat -> "cat"