# MSBuild + Custom Targets in a NuGet Package

I was curious as to how you can run custom targets that were part of a NuGet package when building your project. e.g. when taking a dependancy on a package and building your project, the package you have taken a dependancy on will run some code during the build.

This project shows two ways this can be achieved - when running `dotnet build` on the `Testing.fsproj` the following happens:

```fsharp
Microsoft (R) Build Engine version 16.3.0+0f4c62fea for .NET Core
Copyright (C) Microsoft Corporation. All rights reserved.

  Restore completed in 30.37 ms for C:\Users\Paul\source\repos\MSBuild.CustomTarget\testing\Testing.fsproj.
  Testing -> C:\Users\Paul\source\repos\MSBuild.CustomTarget\testing\bin\Debug\netcoreapp3.0\Testing.dll

  // This is running a Target from the Target.Task.0.0.1 package
  Looked for Task source .dll at: C:\Users\Paul\.nuget\packages\target.task\0.0.1\build\../lib/netstandard2.1/Target.Task.dll
  Hello, you have run a custom Target -> Task from a nuget package during your build in project named 'Testing'

  // This is running a Target from the Target.Exec.0.0.1 package
  You have run an Target excutable from a nuget package in your project 'Testing'
```
The full guide to [MSBuild](https://docs.microsoft.com/en-us/visualstudio/msbuild/msbuild?view=vs-2019) and some of the offical [custom SDKs](https://github.com/microsoft/MSBuildSdks).

### 1. Pack with a `Task`
This is the most simple - see the `Target.Task` project.

### 2. Pack a netcoreapp `.dll` to run with `dotnet`

This uses a `netcoreapp3.0` that can be run using the `exec` command - see the `Target.Exec` project.

By using `<NuspecFile>Target.Exec.nuspec</NuspecFile>` and creating a custom [.nuspec](https://docs.microsoft.com/en-us/nuget/reference/nuspec) file allows this can be packed and run & referenced from a netstandard2.1 project.

### Custom NuGet

In order to use a local version of the packages, use the `RestoreSource` xml property in something like:

```xml
<RestoreSources>https://api.nuget.org/v3/index.json;../Target.Task/bin/$(Config);../Target.Exec/bin/$(Config)</RestoreSources>
```

If you package again and do not increment the package build it will not get replaced in the local NuGet store when running a restore: so you need to delete from `C:/Users/User/.nuget` etc. NuGet cli [reference](https://docs.microsoft.com/en-us/nuget/reference/nuget-exe-cli-reference).

```fsharp
// Show all sources
dotnet nuget locals all --list

// Clear all local cache
dotnet nuget locals all --clear
```

### Folder Structure

Having a folder named `build` is the key to getting this to work. The project that is going to packed must have the following folder structure and naming convention:

```
MyProject.fsproj
./build
    MyProject.props
    MyProject.target
MyFiles.fs
```