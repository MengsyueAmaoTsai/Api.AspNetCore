namespace RichillCapital.Api.AcceptanceTests.Orders;

public sealed class ListOrdersTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ListOrders()
    {
    }
}