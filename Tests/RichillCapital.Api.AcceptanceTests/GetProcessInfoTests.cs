using System.Net;

using FluentAssertions;

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
    }
}