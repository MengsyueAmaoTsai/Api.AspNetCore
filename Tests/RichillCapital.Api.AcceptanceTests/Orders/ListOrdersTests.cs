using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Orders;

namespace RichillCapital.Api.AcceptanceTests.Orders;

public sealed class ListOrdersTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ListOrders()
    {
        var orders = await Client.GetFromJsonAsync<IEnumerable<OrderResponse>>("api/v1/orders");

        orders.Should().NotBeNullOrEmpty();
    }
}