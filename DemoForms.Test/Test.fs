module DemoForms.Test
open NUnit.Framework
open FsUnit
open Model

[<TestFixture>]
type Tests() = 
    let createViewModel () = DemoViewModel()

    let setEntry todo (vm: DemoViewModel) = 
        vm.Entry <- todo
        vm

    let getTodoForString todo (vm: DemoViewModel) = 
        let todo = vm.Todos |> Seq.filter (fun x -> x.Action = todo) |> Seq.head
        (todo, vm)

    let completeTodo (item, vm: DemoViewModel) = 
        vm.SelectedItem <- item
        vm

    let addTodo (vm: DemoViewModel) = 
        vm.AddTodo.Execute null 
        vm

    [<Test>]
    member this.``Command Adds Entry to list`` () =

        let todo = "my todo"
        let vm = createViewModel ()
        vm.Entry <- todo

        vm.AddTodo.Execute null

        Assert.IsTrue(1 = vm.Todos.Count)

    [<Test>]
    member this.``Command with builder pattern`` () =
            
        let todo = "my todo"
        let vm = 
            createViewModel() 
            |> setEntry todo
            |> addTodo

        vm.Todos |> Seq.head |> (fun x -> x.Action = todo) |> Assert.IsTrue

    [<Test>]
    member this.``Command with FsUnit`` () =
            
        let todo = "my todo"
        let vm = 
            createViewModel() 
            |> setEntry todo
            |> addTodo

        vm.Todos |> Seq.head |> (fun x -> x.Action) |> should be (equal todo)

    [<Test>]
    member this.``Create Todo And Mark As Completed`` () =
            
        let todo = "my todo"

        let vm = 
            createViewModel() 
            |> setEntry todo 
            |> addTodo 
            |> getTodoForString todo 
            |> completeTodo

        let todo = vm.Todos |> Seq.head

        todo.Status |> should be (equal Completed)


