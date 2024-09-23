using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Orders;

namespace RichillCapital.Api.AcceptanceTests.Orders;

public sealed class CreateOrderTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Theory]
    [InlineData("TAIFEX:TXF", "Buy", "Market", "IOC", 10)]
    public async Task Should_CreateOrder(
        string symbol,
        string tradeType,
        string orderType,
        string timeInForce,
        decimal quantity)
    {
        var request = new CreateOrderRequest
        {
            AccountId = "000-8283782",
            Symbol = symbol,
            TradeType = tradeType,
            OrderType = orderType,
            TimeInForce = timeInForce,
            Quantity = quantity,
        };

        var response = await Client.PostAsJsonAsync("api/v1/orders", request);
        response.EnsureSuccessStatusCode();

        var created = await response.Content.ReadFromJsonAsync<OrderCreatedResponse>();

        created.Should().NotBeNull();
        created!.Id.Should().NotBeEmpty();
    }
}