using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.SignalSources;

namespace RichillCapital.Api.AcceptanceTests.SignalSources;

public sealed class ListSignalSourcesTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ListSignalSources()
    {
        var signalSources = await Client.GetFromJsonAsync<IEnumerable<SignalSourceResponse>>("api/v1/signal-sources");

        signalSources.Should().NotBeNullOrEmpty();
    }
}