namespace DemoForms

open Xamarin.Forms
open Xamarin.Forms.Xaml

type DemoFormsPage() =
    inherit ContentPage()

    let _ = base.LoadFromXaml(typeof<DemoFormsPage>)

