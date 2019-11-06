[<EntryPoint>]
let main argv =
    let projectName = string argv.[0]
    printfn "You have run an Target excutable from a nuget package in your project '%s'" projectName
    0 // return an integer exit code