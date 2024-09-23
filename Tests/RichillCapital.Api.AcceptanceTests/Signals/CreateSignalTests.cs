using System.Net.Http.Json;

using FluentAssertions;
using FluentAssertions.Extensions;

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
            Symbol = "BINANCE:BTCUSDT",
            TradeType = TradeType.Buy.Name,
            OrderType = OrderType.Market.Name,
            Quantity = 0.01m,
        };

        var response = await Client.PostAsJsonAsync("api/v1/signals", request);
        response.EnsureSuccessStatusCode();

        var created = await response.Content.ReadFromJsonAsync<SignalCreatedResponse>();

        created.Should().NotBeNull();
        created!.Id.Should().NotBeEmpty();

        var signal = await Client.GetFromJsonAsync<SignalDetailsResponse>($"api/v1/signals/{created!.Id}");

        signal.Should().NotBeNull();
        signal!.Id.Should().Be(created!.Id);
        signal!.SourceId.Should().Be(request.SourceId);
        signal!.Origin.Should().Be(request.Origin);
        signal!.Symbol.Should().Be(request.Symbol);
        signal!.TradeType.Should().Be(request.TradeType);
        signal!.Quantity.Should().Be(request.Quantity);
        signal!.Latency.Should().BeGreaterOrEqualTo(0);
        signal!.Status.Should().Be(SignalStatus.Emitted.Name);
        signal!.CreatedTimeUtc.Should().BeCloseTo(DateTimeOffset.UtcNow, 2.Seconds());
    }
}