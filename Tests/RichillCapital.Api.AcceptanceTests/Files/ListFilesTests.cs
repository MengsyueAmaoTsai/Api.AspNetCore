using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Files;

namespace RichillCapital.Api.AcceptanceTests.Files;

public sealed class ListFilesTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ReturnFiles()
    {
        var response = await Client.GetAsync("api/v1/files");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var files = await response.Content.ReadFromJsonAsync<IEnumerable<FileResponse>>();

        files.Should().NotBeNull();
        files.Should().BeEmpty();
    }
}