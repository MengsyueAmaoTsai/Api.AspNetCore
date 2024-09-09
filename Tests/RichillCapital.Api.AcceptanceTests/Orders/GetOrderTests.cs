namespace RichillCapital.Api.AcceptanceTests.Orders;

public sealed class GetOrderTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ReturnOrder()
    {
    }
}