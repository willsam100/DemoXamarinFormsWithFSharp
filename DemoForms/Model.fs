module DemoForms.Model

type Status = 
    | Completed 
    | Current

type TodoItem = {
    Action: string 
    Status: Status
}
 with 
    override this.ToString() = sprintf "T: %s, S: %A" this.Action this.Status

type Item = 
    | Todo of TodoItem 
    | Note of string
        override x.ToString() =
            match x with 
            | Todo x -> x.ToString()
            | Note x -> x

let newTodo task =
    {Action = task; Status = Current}

let completeTodo task = 
    {task with Status = Completed}