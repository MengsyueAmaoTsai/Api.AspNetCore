using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Orders;

namespace RichillCapital.Api.AcceptanceTests.Orders;

public sealed class GetOrderTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ReturnOrder()
    {
        var orderId = "1";
        var order = await Client.GetFromJsonAsync<OrderDetailsResponse>($"api/v1/orders/{orderId}");

        order.Should().NotBeNull();
        order!.Id.Should().Be(orderId);
    }
}