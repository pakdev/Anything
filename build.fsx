
#I "packages/FAKE/tools"
#r "packages/FAKE/tools/FakeLib.dll"

open Fake

let buildDir = "./build/"

Target "Clean" (fun _ ->
    CleanDir buildDir
)

Target "BuildApp" (fun _ ->
    !! "**/*.csproj"
        |> MSBuildRelease buildDir "Build"
        |> Log "AppBuild-Output: "
)

Target "Default" (fun _ ->
    trace "Hello world"
)

"Clean"
    ==> "BuildApp"
    ==> "Default"

RunTargetOrDefault "Default"