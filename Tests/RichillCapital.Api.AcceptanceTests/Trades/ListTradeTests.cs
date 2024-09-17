using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Trades;

namespace RichillCapital.Api.AcceptanceTests.Trades;

public sealed class ListTradeTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ListTrades()
    {
        var trades = await Client.GetFromJsonAsync<IEnumerable<TradeResponse>>("api/v1/trades");

        trades.Should().NotBeNullOrEmpty();
    }
}