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
        let task = vm.Todos |> Seq.filter (fun x -> x.Task = task) |> Seq.head
        (task, vm)

    let completeTask (item, vm: DemoViewModel) = 
        vm.SelectedItem <- item
        vm

    let addTask (vm: DemoViewModel) = 
        vm.AddTask.Execute null 
        vm

    [<Test>]
    member this.``Command Adds Entry to list`` () =

        let task = "my task"
        let vm = createViewModel ()
        vm.Entry <- task

        vm.AddTask.Execute null

        Assert.IsTrue(1 = vm.Todos.Count)

    [<Test>]
    member this.``Command with builder pattern`` () =
            
        let task = "my task"
        let vm = createViewModel() |> setEntry task

        vm.AddTask.Execute null

        vm.Todos |> Seq.head |> (fun x -> x.Task = task) |> Assert.IsTrue

    [<Test>]
    member this.``Command with FsUnit`` () =
            
        let task = "my task"
        let vm = createViewModel() |> setEntry task

        vm.AddTask.Execute null

        vm.Todos |> Seq.head |> (fun x -> x.Task) |> should be (equal task)

    [<Test>]
    member this.``Create Task And Mark As Completed`` () =
            
        let task = "my task"

        let vm = 
            createViewModel() 
            |> setEntry task 
            |> addTask 
            |> getTaskForString task 
            |> completeTask

        let task = vm.Todos |> Seq.head

        task.Status |> should be (equal Completed)


