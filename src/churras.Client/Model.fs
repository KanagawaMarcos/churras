module Churras.Client.Model

open System
open Bolero
open System.Security 

/// Routing endpoints definition.
type Page =
    | [<EndPoint "/">] Login
    | [<EndPoint "/Home">] Home
    | [<EndPoint "/counter">] Counter
    | [<EndPoint "/data">] Data
    
/// The Elmish application's model.
type Model =
    {
        page: Page
        counter: int
        books: Book[] option
        error: string option
        authenticated: bool
    }
and Book =
    {
        title: string
        author: string
        publishDate: DateTime
        isbn: string
    }
and UnauthenticatedUser =
    {
        username: string
        password: SecureString
    }
and AuthenticatedUser =
    {
        username: string
        email: string
        phone: string
    }
and Stock =
    {
        ipo: DateTime
        description: string
        notes: string list
    }
and ShareHolder =
    {
        account: AuthenticatedUser
    }  
and Share =
    {
        uuid: string
        price: decimal
        BoughtAt: DateTime
        owner: ShareHolder
    }
and ShareSold =
    {
        uuid: string
        price: decimal
        soldAt: DateTime
        owner: ShareHolder
    }

