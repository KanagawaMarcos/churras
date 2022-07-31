module churras.Client.Main

open Bolero
open Microsoft.AspNetCore.Components
open Elmish
open System.Net.Http
open Churras.Client.Model
open Churras.Client.View
open Churras.Client.Update

let initModel =
    {
        page = Home
        counter = 0
        books = None
        error = None
        authenticated = false
    }


type MyApp() =
    inherit ProgramComponent<Model, Message>()

    [<Inject>]
    member val HttpClient = Unchecked.defaultof<HttpClient> with get, set

    override this.Program =
        let update = update this.HttpClient
        Program.mkProgram (fun _ -> initModel, Cmd.ofMsg GetBooks) update view
        |> Program.withRouter router
