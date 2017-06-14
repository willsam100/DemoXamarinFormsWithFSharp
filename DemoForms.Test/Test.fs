module DemoForms.Test
open NUnit.Framework
open FsUnit
open Model

[<TestFixture>]
type Tests() = 
    let createViewModel () = DemoViewModel()

    let setEntry task (vm: DemoViewModel) = 
        vm.Entry <- task
        vm

    let getTaskForString task (vm: DemoViewModel) = 

        let getText = function 
            | Todo x -> x.Action
            | Note x -> x

        let task = vm.Todos |> Seq.filter (fun x -> x |> getText = task) |> Seq.head
        (task, vm)

    let completeTask (item, vm: DemoViewModel) = 
        vm.SelectedItem <- item
        vm

    let addTask (vm: DemoViewModel) = 
        vm.AddTodo.Execute null 
        vm

    // Arguments can be ommited if the a function is returned. type is: getTask(item: Item): String
    let getTask = function
    | Todo x -> x.Action
    | Note x -> failwith "Not a task"

    [<Test>]
    member this.``Command Adds Entry to list`` () =

        let task = "my task"
        let vm = createViewModel ()
        vm.Entry <- task

        vm.AddTodo.Execute null

        Assert.IsTrue(1 = vm.Todos.Count)

    [<Test>]
    member this.``Command with builder pattern`` () =
            
        let task = "my task"
        let vm = 
            createViewModel() 
            |> setEntry task
            |> addTask

        vm.Todos |> Seq.head |> getTask = task |> Assert.IsTrue

    [<Test>]
    member this.``Command with FsUnit`` () =
            
        let task = "my task"
        let vm = 
            createViewModel() 
            |> setEntry task
            |> addTask

        vm.Todos |> Seq.head |> getTask |> should be (equal task)

    [<Test>]
    member this.``Create Task And Mark As Completed`` () =
            
        let todo = "my task"

        let vm = 
            createViewModel() 
            |> setEntry todo 
            |> addTask 
            |> getTaskForString todo 
            |> completeTask

        let task = vm.Todos |> Seq.head

        match task with 
        | Todo x -> x.Status |> should be (equal Completed)
        | _ -> failwith "Not a task"


