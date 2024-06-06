namespace RichillCapital.Api.AcceptanceTests;

public abstract class AcceptanceTest : IClassFixture<AcceptanceTestWebApplicationFactory>
{
    protected readonly HttpClient Client;

    protected AcceptanceTest(AcceptanceTestWebApplicationFactory factory) =>
        Client = factory.CreateClient();
}
