open Microsoft.Extensions.Configuration

[<EntryPoint>]
let main argv =

    let projectName = argv.[0]
    let userSecretsId = Array.tryItem 1 argv
    let secretKeys =
        Array.tryItem 2 argv
        |> Option.map (fun keys -> keys.Split("--::--"))

    printfn "--------------------------------------------------------------------------------------------"
    printfn "You have run an Target excutable from a nuget package in your project '%s'" projectName

    match userSecretsId, secretKeys with
    | Some userSecretsId, Some secretKeys ->
        printfn "I will now print all your User Secrets:"
        let config = ConfigurationBuilder().AddUserSecrets(userSecretsId).Build().AsEnumerable() |> System.Collections.Generic.Dictionary
        let printValue (key : string) =
            match config.TryGetValue key with
            | true, value -> printfn "The User Secret with Key: %s has a value of: %s" key value
            | false, _ -> printfn "There is no User Secret with a Key: %s" key
        for key in secretKeys do printValue key
    | _ -> ()

    printfn "--------------------------------------------------------------------------------------------"
    printfn "%s" System.Environment.NewLine
    0 // return an integer exit code