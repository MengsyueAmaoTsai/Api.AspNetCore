using System.Reflection;

using FluentAssertions;

using NetArchTest.Rules;

using RichillCapital.UseCases;

namespace RichillCapital.Api.ArchitectureTests;

public sealed class ApplicationTests
{
    private static readonly Assembly UseCasesAssembly = typeof(ApplicationExtensions).Assembly;

    [Fact]
    public void Should_NotHaveDependency_On_PresentationLayer()
    {
        // Arrange
        var presentationProjects = new List<string>
        {
            "RichillCapital.Api",
            "RichillCapital.Contracts",
        };

        // Act
        var results = presentationProjects
            .Select(project => Types.InAssembly(UseCasesAssembly)
                .ShouldNot()
                .HaveDependencyOn(project)
                .GetResult());

        // Assert
        results.Should().OnlyContain(result => result.IsSuccessful);
    }

    [Fact]
    public void Should_NotHaveDependency_On_InfrastructureLayer()
    {
        // Arrange
        var infrastructureProjects = new List<string>
        {
            "RichillCapital.Persistence",
            "RichillCapital.Identity",
            "RichillCapital.Logging",
        };

        // Act
        var results = infrastructureProjects
            .Select(project => Types.InAssembly(UseCasesAssembly)
                .ShouldNot()
                .HaveDependencyOn(project)
                .GetResult());

        // Assert
        results.Should().OnlyContain(result => result.IsSuccessful);
    }
}