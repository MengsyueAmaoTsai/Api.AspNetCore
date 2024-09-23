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

var dotNetTestSettings = new DotNetTestSettings
{
    Configuration = buildConfiguration,
    NoBuild = true,
    NoRestore = true,
};

Task("UnitTests")
    .Does(() =>
    {
        var projects = new List<string>
        {
            "./Tests/RichillCapital.Domain.UnitTests/RichillCapital.Domain.UnitTests.csproj",
        };

        foreach (var project in projects)
        {
            Information("Running unit tests for {0}", project);

            DotNetTest(project, dotNetTestSettings);
        }
    });

Task("AcceptanceTests")
    .Does(() =>
    {
        var projects = new List<string>
        {
            "./Tests/RichillCapital.Api.AcceptanceTests/RichillCapital.Api.AcceptanceTests.csproj",
        };

        foreach (var project in projects)
        {
            Information("Running acceptance tests for {0}", project);

            DotNetTest(project, dotNetTestSettings);
        }
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


Task("Commit")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("UnitTests");

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("UnitTests")
    .IsDependentOn("AcceptanceTests")
    .IsDependentOn("Publish");


var target = Argument("target", "Default");
RunTarget(target);