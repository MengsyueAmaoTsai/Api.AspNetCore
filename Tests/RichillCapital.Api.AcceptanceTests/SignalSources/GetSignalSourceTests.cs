using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts.SignalSources;
using RichillCapital.Persistence.Seeds;

namespace RichillCapital.Api.AcceptanceTests.SignalSources;

public sealed class GetSignalSourceTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Theory]
    [InlineData("not-existing-source-id", HttpStatusCode.NotFound, "SignalSources.NotFound")]
    public async Task When_GivenInvalidRequest_Should_ReturnError(
        string sourceId,
        HttpStatusCode expectedStatusCode,
        string expectedTitle)
    {
        var response = await Client.GetAsync($"api/v1/signal-sources/{sourceId}");
        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        problem.Should().NotBeNull();
        problem!.Status.Should().Be((int)expectedStatusCode);
        problem!.Title.Should().Be(expectedTitle);
    }

    [Fact]
    public async Task When_GivenValidRequest_Should_ReturnSignalSource()
    {
        var sourceId = "TV-Long-Task";
        var expectedSource = Seed.CreateSignalSources().First(x => x.Id.Value == sourceId);

        var signalSource = await Client.GetFromJsonAsync<SignalSourceResponse>($"api/v1/signal-sources/{sourceId}");
        signalSource.Should().NotBeNull();
        signalSource!.Id.Should().Be(expectedSource.Id.Value);
        signalSource!.Name.Should().Be(expectedSource.Name);
        signalSource!.Description.Should().Be(expectedSource.Description);
    }
}