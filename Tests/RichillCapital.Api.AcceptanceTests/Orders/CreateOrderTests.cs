namespace RichillCapital.Api.AcceptanceTests.Orders;

public sealed class CreateOrderTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_CreateOrder()
    {
    }
}