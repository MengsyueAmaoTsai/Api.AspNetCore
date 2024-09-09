namespace RichillCapital.Api.AcceptanceTests;

public abstract class AcceptanceTest : IClassFixture<EndToEndTestWebApplicationFactory>
{
    protected readonly HttpClient Client;

    protected AcceptanceTest(EndToEndTestWebApplicationFactory factory) =>
        Client = factory.CreateClient();
}
