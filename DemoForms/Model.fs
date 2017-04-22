module DemoForms.Model

type Status = 
    | Completed 
    | Current

type TodoItem = {
    Task: string 
    Status: Status
}
 with 
    override this.ToString() = sprintf "T: %s, S: %A" this.Task this.Status

let newTask task =
    {Task = task; Status = Current}

let completeTask task = 
    {task with Status = Completed}