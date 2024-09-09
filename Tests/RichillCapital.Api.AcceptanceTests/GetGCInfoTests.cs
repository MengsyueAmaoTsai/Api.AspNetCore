using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts;

namespace RichillCapital.Api.AcceptanceTests.Endpoints;

public sealed class GetGCInfoTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_Return_GCInfo()
    {
        var response = await Client.GetAsync("gc-info");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var gcInfo = await response.Content.ReadFromJsonAsync<GCInfoResponse>();

        gcInfo.Should().NotBeNull();
        gcInfo?.MachineName.Should().NotBeNullOrEmpty();
    }
}