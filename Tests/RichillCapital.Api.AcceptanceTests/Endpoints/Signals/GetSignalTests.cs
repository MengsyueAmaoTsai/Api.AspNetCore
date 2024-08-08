using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Signals;
using RichillCapital.Persistence.Seeds;

namespace RichillCapital.Api.AcceptanceTests.Endpoints.Signals;

public sealed class GetSignalTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ReturnSignal()
    {
        var expected = Seed.CreateSignals().First();

        var signal = await Client.GetFromJsonAsync<SignalDetailsResponse>($"api/v1/signals/{expected.Id}");
        signal.Should().NotBeNull();
        signal!.Id.Should().Be(expected.Id.Value);
    }
}
