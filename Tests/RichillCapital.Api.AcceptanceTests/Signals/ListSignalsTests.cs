using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Signals;

namespace RichillCapital.Api.AcceptanceTests.Signals;

public sealed class ListSignalsTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ListSignals()
    {
        var signals = await Client.GetFromJsonAsync<IEnumerable<SignalResponse>>("api/v1/signals");

        signals.Should().NotBeNullOrEmpty();
    }
}
