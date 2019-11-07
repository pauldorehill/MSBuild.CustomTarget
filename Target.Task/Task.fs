namespace Target.Task

open Microsoft.Build.Utilities
open Microsoft.Build.Framework

type MyCustomTask() =
    inherit Task()
    [<Required>] member val ProjectName = "" with get, set
    member this.LogMessageHigh msg = this.Log.LogMessage(MessageImportance.High, msg)
    override this.Execute() =
        this.ProjectName
        |> sprintf "A Target has sucessfully called a Task when building your project called '%s'"
        |> this.LogMessageHigh
        this.LogMessageHigh "--------------------------------------------------------------------------------------------"
        this.LogMessageHigh System.Environment.NewLine
        true