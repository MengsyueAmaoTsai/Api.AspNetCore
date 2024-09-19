using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Signals;
using RichillCapital.Domain;

namespace RichillCapital.Api.AcceptanceTests.Signals;

public sealed class CreateSignalTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_CreateSignal()
    {
        var request = new CreateSignalRequest
        {
            Time = DateTimeOffset.UtcNow,
            Origin = SignalOrigin.TradingView.Name,
            SourceId = "TV-Long-Task",
        };

        var response = await Client.PostAsJsonAsync("api/v1/signals", request);
        response.EnsureSuccessStatusCode();

        var created = await response.Content.ReadFromJsonAsync<SignalCreatedResponse>();

        created.Should().NotBeNull();
        created!.Id.Should().NotBeEmpty();
    }
}