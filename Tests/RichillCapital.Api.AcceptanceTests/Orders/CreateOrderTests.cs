using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Orders;

namespace RichillCapital.Api.AcceptanceTests.Orders;

public sealed class CreateOrderTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_CreateOrder()
    {
        var request = new CreateOrderRequest
        {
            AccountId = "1",
            Symbol = "BINANCE:BTCUSDT",
            TradeType = "Buy",
            Quantity = 1,
            OrderType = "Market",
            TimeInForce = "GTC",
        };

        var response = await Client.PostAsJsonAsync("api/v1/orders", request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<CreateOrderResponse>();
        result.Should().NotBeNull();
        result!.OrderId.Should().NotBeEmpty();
    }
}