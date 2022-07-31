module Churras.Client.View

open Bolero
open Bolero.Html
open Churras.Client.Model
open Churras.Client.Update
open Bolero.Attr

type Main = Template<"wwwroot/main.html">

let homePage model dispatch =
    Main.Home().Elt()

let counterPage model dispatch =
    Main.Counter()
        .Decrement(fun _ -> dispatch Decrement)
        .Increment(fun _ -> dispatch Increment)
        .Value(model.counter, fun v -> dispatch (SetCounter v))
        .Elt()

let dataPage model dispatch =
    Main.Data()
        .Reload(fun _ -> dispatch GetBooks)
        .Rows(cond model.books <| function
            | None ->
                Main.EmptyData().Elt()
            | Some books ->
                forEach books <| fun book ->
                    tr {
                        td { book.title }
                        td { book.author }
                        td { book.publishDate.ToString("yyyy-MM-dd") }
                        td { book.isbn }
                    })
        .Elt()

let loginPage model dispatch =
    div {
        attr.``class`` "login-grid"
        attr.id "login-grid-id"
        div {empty()}
        div {
            h1 {
                attr.``class`` "page-title"
                attr.id "page-title-id"
                "Agenda de Churras"
            }
            
            form {
                div {
                    attr.``class`` "login-form"
                    attr.id "login-form-id"
                    label{
                        "Email"
                    }
                    input {
                        attr.``type`` "email"
                    }
                    label{
                        "Password"
                    }
                    input {
                        attr.``type`` "password"
                    }
                    button{
                        "Login"
                        a
                    }
                }
            }
        }
        div {empty()}
    }
let menuItem (model: Model) (page: Page) (text: string) =
    Main.MenuItem()
        .Active(if model.page = page then "is-active" else "")
        .Url(router.Link page)
        .Text(text)
        .Elt()

let view model dispatch =
    Main()
        .Menu(concat {
            menuItem model Login "Login"
            menuItem model Home "Home"
            menuItem model Counter "Counter"
            menuItem model Data "Download data"
        })
        .Body(
            cond model.page <| function
            | Login -> loginPage model dispatch
            | Home -> homePage model dispatch
            | Counter -> counterPage model dispatch
            | Data ->
                dataPage model dispatch
        )
        .Error(
            cond model.error <| function
            | None -> empty()
            | Some err ->
                Main.ErrorNotification()
                    .Text(err)
                    .Hide(fun _ -> dispatch ClearError)
                    .Elt()
        )
        .Elt()
