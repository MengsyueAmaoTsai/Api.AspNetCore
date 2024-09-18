using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts;

namespace RichillCapital.Api.AcceptanceTests.Endpoints;

public sealed class GetProcessInfoTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ReturnProcessInfo()
    {
        var response = await Client.GetAsync("process-info");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var processInfo = await response.Content.ReadFromJsonAsync<ProcessInfoResponse>();

        processInfo.Should().NotBeNull();
        processInfo?.MachineName.Should().NotBeNullOrEmpty();
        processInfo!.UserName.Should().NotBeNullOrEmpty();
    }
}