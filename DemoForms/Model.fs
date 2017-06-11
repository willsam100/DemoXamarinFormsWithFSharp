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

let newTodo todo =
    {Action = todo; Status = Current}

let completeTodo todo = 
    {todo with Status = Completed}