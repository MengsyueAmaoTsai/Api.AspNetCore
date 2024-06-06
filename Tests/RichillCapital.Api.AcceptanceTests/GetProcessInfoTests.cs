using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Api.Endpoints;

namespace RichillCapital.Api.AcceptanceTests;

public sealed class GetProcessInfoTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_Return_GCInfo()
    {
        var response = await Client.GetAsync("process-info");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var processInfo = await response.Content.ReadFromJsonAsync<ProcessInfoResponse>();

        processInfo.Should().NotBeNull();
    }
}