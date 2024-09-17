using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Orders;
using RichillCapital.Domain;

namespace RichillCapital.Api.AcceptanceTests.Orders;

public sealed class CreateOrderTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_CreateOrder()
    {
        var createRequest = new CreateOrderRequest
        {
            AccountId = "1",
            Symbol = "AAPL",
            TradeType = TradeType.Buy.Name,
            OrderType = OrderType.Market.Name,
            TimeInForce = TimeInForce.ImmediateOrCancel.Name,
            Quantity = 10,
        };

        var response = await Client.PostAsJsonAsync("api/v1/orders", createRequest);
        response.EnsureSuccessStatusCode();

        var created = await response.Content.ReadFromJsonAsync<OrderCreatedResponse>();

        created.Should().NotBeNull();
        created!.Id.Should().NotBeEmpty();

        var order = await Client.GetFromJsonAsync<OrderDetailsResponse>($"api/v1/orders/{created!.Id}");

        order.Should().NotBeNull();
        order!.Id.Should().Be(created!.Id);
        order.AccountId.Should().Be(createRequest.AccountId);
        order.Symbol.Should().Be(createRequest.Symbol);
        order.TradeType.Should().Be(createRequest.TradeType);
        order.Type.Should().Be(createRequest.OrderType);
        order.TimeInForce.Should().Be(createRequest.TimeInForce);
        order.Quantity.Should().Be(createRequest.Quantity);
        order.CreatedTimeUtc.Should().NotBe(default);
    }
}