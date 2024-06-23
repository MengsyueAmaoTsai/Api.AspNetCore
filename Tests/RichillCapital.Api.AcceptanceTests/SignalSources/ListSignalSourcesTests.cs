using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts;
using RichillCapital.Contracts.SignalSources;

namespace RichillCapital.Api.AcceptanceTests.SignalSources;

public sealed class ListSignalSourcesTests(
    AcceptanceTestWebApplicationFactory factory)
    : AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ReturnSignalSources()
    {
        var signalSources = await Client.GetFromJsonAsync<Paged<SignalSourceResponse>>("api/v1/signal-sources");

        signalSources.Should().NotBeNull();
        signalSources!.Items.Should().NotBeEmpty().And.HaveCount(6);
    }
}
