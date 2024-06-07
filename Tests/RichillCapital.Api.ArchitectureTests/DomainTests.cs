using System.Reflection;

using FluentAssertions;

using NetArchTest.Rules;

using RichillCapital.Domain;

namespace RichillCapital.Api.ArchitectureTests;

public sealed class DomainTests
{
    private static readonly Assembly DomainAssembly = typeof(User).Assembly;

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
            .Select(project => Types.InAssembly(DomainAssembly)
                .ShouldNot()
                .HaveDependencyOn(project)
                .GetResult());

        // Assert
        results.Should().OnlyContain(result => result.IsSuccessful);
    }

    [Fact]
    public void Should_NotHaveDependency_On_ApplicationLayer()
    {
        // Act
        var results = Types.InAssembly(DomainAssembly)
                .ShouldNot()
                .HaveDependencyOn("RichillCapital.UseCases")
                .GetResult();

        // Assert
        results.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Should_NotHaveDependency_On_InfrastructureLayer()
    {
        // Arrange
        var applicationAssembly = typeof(User).Assembly;

        var infrastructureProjects = new List<string>
        {
            "RichillCapital.Persistence",
            "RichillCapital.Identity",
            "RichillCapital.Logging",
        };

        // Act
        var results = infrastructureProjects
            .Select(project => Types.InAssembly(applicationAssembly)
                .ShouldNot()
                .HaveDependencyOn(project)
                .GetResult());

        // Assert
        results.Should().OnlyContain(result => result.IsSuccessful);
    }
}