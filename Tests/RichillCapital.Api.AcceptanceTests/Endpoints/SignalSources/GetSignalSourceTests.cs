using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.SignalSources;

namespace RichillCapital.Api.AcceptanceTests.Endpoints.SignalSources;

public sealed class GetSignalSourceTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ReturnSignalSource()
    {
        var sourceId = "1";

        var signalSource = await Client.GetFromJsonAsync<SignalSourceDetailsResponse>($"api/v1/signal-sources/{sourceId}");
        signalSource.Should().NotBeNull();
        signalSource!.Id.Should().Be(sourceId);
    }
}
