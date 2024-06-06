using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts;

namespace RichillCapital.Api.AcceptanceTests;

public sealed class GetThreadPoolInfoTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_Return_GCInfo()
    {
        var response = await Client.GetAsync("thread-pool-info");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var info = await response.Content.ReadFromJsonAsync<ThreadPoolInfoResponse>();

        info.Should().NotBeNull();
        info?.MachineName.Should().NotBeNullOrEmpty();
    }
}