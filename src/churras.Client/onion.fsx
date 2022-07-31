﻿// 1. pure, don't think about IO at all
module Domain =
    let add x y = x + y

// 2. think about IO but not its implementation
module App =
    let add (getX: unit -> Async<int32>) y =
        async {
            let! x = getX ()
            return Domain.add x y
        }

// 3. IO implementation
module Infra =
    let getX () = async { return 7 }

// 4. inject dependencies
module Startup =
    let add = App.add Infra.getX

// demo
Startup.add 3
|> Async.RunSynchronously
|> printfn "%A" // 10