using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Orders;

namespace RichillCapital.Api.AcceptanceTests.Orders;

public sealed class ListOrdersTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_CreateOrder()
    {
        var request = new CreateOrderRequest
        {
            AccountId = "1",
            Symbol = "AAPL",
            TradeType = "Buy",
            OrderType = "Market",
            TimeInForce = "IOC",
            Quantity = 10,
        };

        var response = await Client.PostAsJsonAsync("api/v1/orders", request);
        response.EnsureSuccessStatusCode();

        var created = await response.Content.ReadFromJsonAsync<OrderCreatedResponse>();

        created.Should().NotBeNull();
        created!.Id.Should().NotBeEmpty();
    }
}