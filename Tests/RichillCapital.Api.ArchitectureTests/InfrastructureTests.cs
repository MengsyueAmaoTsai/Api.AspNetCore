using System.Reflection;

using FluentAssertions;

using NetArchTest.Rules;

using RichillCapital.Logging;
using RichillCapital.Persistence;

namespace RichillCapital.Api.ArchitectureTests;

public sealed class InfrastructureTests
{
    private static readonly Assembly[] InfrastructureAssemblies =
    [
        typeof(DatabaseExtensions).Assembly,
        typeof(LoggingExtensions).Assembly,
    ];

    [Fact]
    public void Should_Not_Have_Dependency_On_PresentationLayer()
    {
        var presentationProjects = new List<string>
        {
            "RichillCapital.Api",
            "RichillCapital.Contracts",
        };

        List<TestResult> results = [];

        foreach (var assembly in InfrastructureAssemblies)
        {
            foreach (var project in presentationProjects)
            {
                results.Add(Types.InAssembly(assembly)
                    .ShouldNot()
                    .HaveDependencyOn(project)
                    .GetResult());
            }
        }

        results.Should().OnlyContain(result => result.IsSuccessful);
    }
}