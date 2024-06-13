using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.SignalSources;

namespace RichillCapital.Api.AcceptanceTests.SignalSources;

public sealed class GetSignalSourceTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task When_GivenValidRequest_Should_ReturnSignalSource()
    {
        var sourceId = "TV-Long-Task";
        var response = await Client.GetAsync($"api/v1/signal-sources/{sourceId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<SignalSourceDetailsResponse>();
        result.Should().NotBeNull();
        result!.Id.Should().Be(sourceId);
    }

}