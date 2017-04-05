module DemoForms.Test
open NUnit.Framework
open FsUnit
open Model

[<TestFixture>]
type Tests() = 

    let setEntry task (vm: DemoViewModel) = 
        vm.Entry <- task
        vm

    let getTaskForString task (vm: DemoViewModel) = 

        let getText = function 
            | Task x -> x.Task
            | Note x -> x

        let task = vm.Todos |> Seq.filter (fun x -> x |> getText = task) |> Seq.head
        (task, vm)

    let complete (item, vm: DemoViewModel) = 
        vm.SelectedItem <- item
        vm

    let addTask (vm: DemoViewModel) = 
        vm.AddTask.Execute null 
        vm

    let getTask = function
    | Task x -> x.Task
    | Note x -> failwith "Not a task"

    [<Test>]
    member this.Command_Adds_Entry_to_list () =

        let task = "my task"
        let vm = DemoViewModel()
        vm.Entry <- task

        vm.Clicked.Execute null

        Assert.IsTrue(1 = vm.Todos.Count)

    [<Test>]
    member this.``Command with builder pattern`` () =
            
        let task = "my task"
        let vm = DemoViewModel() |> setEntry task

        vm.AddTask.Execute null

        let getTask = function
        | Task x -> x.Task
        | Note x -> failwith "Not a task"

        vm.Todos |> Seq.head |> getTask = task |> Assert.IsTrue

    [<Test>]
    member this.``Command with FsUnit`` () =
            
        let task = "my task"
        let vm = DemoViewModel() |> setEntry task

        vm.AddTask.Execute null

        vm.Todos |> Seq.head |> getTask |> should be (equal task)

    [<Test>]
    member this.``Create Task And Mark As Completed`` () =
            
        let task = "my task"

        let vm = 
            DemoViewModel() 
            |> setEntry task 
            |> addTask 
            |> getTaskForString task 
            |> complete

        vm.Todos |> Seq.head |> getTask |> should be (equal task)


