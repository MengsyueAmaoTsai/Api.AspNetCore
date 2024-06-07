namespace RichillCapital.Api.AcceptanceTests.Files;

public sealed class ListFilesTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ReturnFiles()
    {
    }
}