namespace Target.Task

open Microsoft.Build.Utilities
open Microsoft.Build.Framework

type MyCustomTask() =
    inherit Task()
    [<Required>]
    member val ProjectName = "" with get, set
    member this.LogMessageHigh msg = this.Log.LogMessage(MessageImportance.High, msg)
    override this.Execute() =
        this.ProjectName
        |> sprintf "Hello, you have run a custom Target -> Task from a nuget package during your build in project named '%s'"
        |> this.LogMessageHigh
        true