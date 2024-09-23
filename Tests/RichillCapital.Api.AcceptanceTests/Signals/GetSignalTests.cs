using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Signals;

namespace RichillCapital.Api.AcceptanceTests.Signals;

public sealed class GetSignalTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ReturnSignal()
    {
        var id = "1";
        var signal = await Client.GetFromJsonAsync<SignalDetailsResponse>($"api/v1/signals/{id}");

        signal.Should().NotBeNull();
        signal!.Id.Should().NotBeEmpty();
    }
}