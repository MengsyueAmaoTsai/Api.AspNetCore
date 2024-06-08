namespace RichillCapital.Api.AcceptanceTests;

public sealed class TestTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ReturnSuccess()
    {
        var response = await Client.GetAsync("/test");
        response.EnsureSuccessStatusCode();
    }
}