// include Fake lib
#r @"packages\FAKE\tools\FakeLib.dll"
open Fake
open System
open Fake.AssemblyInfoFile

RestorePackages()

type Project = { name: string;  authors: List<string>; description: string; summary: string; tags: string}

let authors = ["Craig Smitham"]

let appFunc= { 
    name = "AppFunc"; 
    authors = authors; 
    summary = "";
    description ="Ultralight framework for building SOLID Application Services.";
    tags = "Messaging Functional DDD Services" }

let csl = { 
    name = "AppFunc.CommonServiceLocator"; 
    authors = authors; 
    summary = "";
    description = "Common Service Locator integration for AppFunc";
    tags = "Autofac IoC " + appFunc.tags }


let projects = [ appFunc; csl  ]

let buildMode = getBuildParamOrDefault "buildMode" "Release"
let testResultsDir = "./testresults"
let packagesDir = "./packages/"
let packagingRoot = "./packaging/"
let projectBins =  projects |> List.map(fun p -> "./src/" @@ p.name @@ "/bin")
let projectPackagingDirs =  projects |> List.map(fun p -> packagingRoot @@ p.name)

let buildNumber = environVarOrDefault "APPVEYOR_BUILD_NUMBER" "0"
// APPVEYOR_BUILD_VERSION:  MAJOR.MINOR.PATCH.BUILD_NUMBER
let buildVersionDefault = "0.0.8.0"
let buildVersion = environVarOrDefault "APPVEYOR_BUILD_VERSION" buildVersionDefault
let majorMinorPatch = split '.' buildVersion  |> Seq.take(3) |> Seq.toArray |> (fun versions -> String.Join(".", versions))
let assemblyVersion = majorMinorPatch
let assemblyFileVersion = buildVersion
let versionSuffix = getBuildParamOrDefault "versionsuffix" ("ci" + buildNumber)
let isRelease = hasBuildParam "release" 
let packageVersion = if isRelease then majorMinorPatch else majorMinorPatch + "-" + versionSuffix 

// Targets
Target "Clean" (fun _ -> 
   List.concat [projectBins; projectPackagingDirs; [testResultsDir; packagingRoot]] |> CleanDirs
)

Target "AssemblyInfo" (fun _ ->
    CreateCSharpAssemblyInfo "./SolutionInfo.cs"
      [ Attribute.Product "AppFunc" 
        Attribute.Version assemblyVersion
        Attribute.FileVersion assemblyFileVersion
        Attribute.ComVisible false ]
)

Target "BuildApp" (fun _ ->
   MSBuild null "Build" ["Configuration", buildMode] ["./AppFunc.sln"] |> Log "AppBuild-Output: "
)


let useDefaults = None
let withCustomParams (configuration: NuGetParams -> NuGetParams) = 
    Some(configuration)

let withPackage = fun packageName -> withCustomParams(fun p -> 
            {p with 
                Dependencies =
                    [packageName, GetPackageVersion packagesDir packageName] })

let createNuGetPackage (project:Project) (customParams: (NuGetParams -> NuGetParams) option) = 
    let packagingDir = (packagingRoot @@ project.name @@ "/")
    let net45Dir =  (packagingDir @@ "lib/net45")
    let buildDir = ("./src/" @@ project.name @@ "/bin")
    let publishUrl = getBuildParamOrDefault "publishurl" (environVarOrDefault "nugetpublishurl" "")
    let apiKey = getBuildParamOrDefault "apikey" (environVarOrDefault "nugetapikey" "")

    CleanDir net45Dir
    CopyFile net45Dir (buildDir @@ "Release/" @@ project.name + ".dll")
    CopyFiles packagingDir ["LICENSE.txt"; "README.md"]

    NuGet((fun p -> 
        {p with 

            Project = project.name
            Authors = project.authors 
            Description = project.description 
            OutputPath = packagingRoot 
            Summary = project.summary 
            WorkingDir = packagingDir
            Version = packageVersion
            Tags = project.tags
            PublishUrl = publishUrl
            AccessKey = apiKey 
            Publish = publishUrl <> "" } 
            |>  match customParams with
                | Some(customParams) -> customParams
                | None -> (fun p -> p))) "./base.nuspec"


Target "CreateCorePackage" (fun _ -> 
    createNuGetPackage appFunc useDefaults
)

Target "CreateCommonServiceLocatorPackage" (fun _ -> 
     createNuGetPackage csl 
        (withCustomParams(fun p -> 
            {p with 
                Dependencies =
                    ["AppFunc", packageVersion] }))
)


Target "ContinuousIntegration" DoNothing
Target "CreatePackages" DoNothing
Target "Default" DoNothing

"Clean"
    ==> "AssemblyInfo"
        ==> "BuildApp"

"BuildApp" 
    ==>"CreateCorePackage"
        ==> "CreatePackages"

"BuildApp" 
    ==>"CreateCommonServiceLocatorPackage"
        ==> "CreatePackages"
        

"BuildApp" 
    ==>"CreatePackages"
        ==> "ContinuousIntegration" 


// start build
RunTargetOrDefault (environVarOrDefault "target" "Default")