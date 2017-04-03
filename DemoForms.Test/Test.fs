module DemoForms.Test
open NUnit.Framework

[<TestFixture>]
type Tests = 

    let setEntry task (vm: DemoViewModel) = 
        vm.Entry <- task
        vm

    [<Test>]
    member this.``Command Adds Entry to list`` () =

        let task = "my task"
        let vm = DemoViewModel()
        vm.Entry <- task

        vm.Clicked.Execute null

        Assert.IsTrue(1 = vm.Todos.Count)

    [<Test>]
    member this.``Command with FSharp power`` () =
            
        let task = "my task"
        let vm = DemoViewModel() |> setEntry task

        vm.Clicked.Execute null

        vm.Todos |> Seq.head = task |> Assert.IsTrue


