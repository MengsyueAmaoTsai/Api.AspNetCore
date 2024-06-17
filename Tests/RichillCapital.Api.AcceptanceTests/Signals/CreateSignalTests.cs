using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Signals;

namespace RichillCapital.Api.AcceptanceTests.Signals;


public sealed class CreateSignalTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Theory]
    [InlineData("buy", "flat", 0, "long", 1)]
    [InlineData("buy", "long", 1, "long", 2)]
    [InlineData("sell", "long", 1, "long", 0.5)]
    [InlineData("sell", "long", 0.5, "flat", 0)]
    [InlineData("sell", "flat", 0, "short", 1)]
    [InlineData("sell", "short", 1, "short", 2)]
    [InlineData("buy", "short", 2, "short", 1)]
    [InlineData("buy", "short", 1, "flat", 0)]

    public async Task When_GivenValidRequest_Should_CreateSignal(
        string tradeType,
        string marketPosition,
        decimal previousMarketPositionSize,
        string previousMarketPosition,
        decimal marketPositionSize)
    {
        var sourceId = "TV-Long-Task";
        var exchange = "BINANCE";
        var symbol = "BTCUSDT";

        var response = await Client.PostAsJsonAsync(
            "api/v1/signals",
            new CreateSignalRequest
            {
                SourceId = sourceId,
                CurrentTime = DateTimeOffset.UtcNow,
                TradeType = tradeType,
                Exchange = exchange,
                Symbol = symbol,
                Price = 10000,
                MarketPosition = marketPosition,
                MarketPositionSize = previousMarketPositionSize,
                PreviousMarketPosition = previousMarketPosition,
                PreviousMarketPositionSize = marketPositionSize,
            });

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<CreateSignalResponse>();
        result.Should().NotBeNull();
        result!.Id.Should().NotBeNullOrWhiteSpace();
    }
}