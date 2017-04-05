module DemoForms.Model
open System

type Status = 
    | Completed 
    | Current

type TodoItem = {
    Task: string 
    Status: Status
}
 with 
    override this.ToString() = sprintf "T: %s, S: %A" this.Task this.Status

type Item = 
    | Task of TodoItem 
    | Note of string
        override x.ToString() =
            match x with 
            | Task x -> x.ToString()
            | Note x -> x

let newTask task =
    {Task = task; Status = Current}

let completeTask task = 
    {task with Status = Completed}