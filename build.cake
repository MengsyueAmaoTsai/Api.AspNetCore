var solution = "./RichillCapital.Api.sln";
var project = "./RichillCapital.Api.csproj";
var buildConfiguration = Argument("configuration", "Release");
var publishDirectory = "./publish";

Task("Clean")
    .Does(() =>
    {
        DotNetClean(solution);
    });

Task("Restore")
    .Does(() =>
    {
        DotNetRestore(solution);
    });

Task("Build")
    .Does(() =>
    {
        DotNetBuild(
            solution,
            new DotNetBuildSettings
            {
                Configuration = buildConfiguration,
                NoRestore = true,
            });
    });

Task("AcceptanceTests")
    .Does(() =>
    {
        DotNetTest(
            "./Tests/RichillCapital.Api.AcceptanceTests/RichillCapital.Api.AcceptanceTests.csproj",
            dotNetTestSettings);
    });

Task("Publish")
    .Does(() =>
    {
        CleanDirectory(publishDirectory);

        DotNetPublish(
            project,
            new DotNetPublishSettings
            {
                Configuration = buildConfiguration,
                NoRestore = true,
                NoBuild = true,
                OutputDirectory = publishDirectory,
            });
    });

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("AcceptanceTests")
    .IsDependentOn("Publish");

var target = Argument("target", "Default");
RunTarget(target);