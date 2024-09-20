using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Brokerages;

namespace RichillCapital.Api.AcceptanceTests.Brokerages;

public sealed class StopBrokerageTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_StartBrokerage()
    {
        var response = await Client.PostAsJsonAsync(
            "api/v1/brokerages/stop", new StopBrokerageRequest
            {
                ConnectionName = "RichillCapital Brokerage Connection"
            });

        response.EnsureSuccessStatusCode();

        var startedBrokerage = await response.Content.ReadFromJsonAsync<BrokerageResponse>();

        startedBrokerage.Should().NotBeNull();
        startedBrokerage!.IsConnected.Should().BeFalse();
    }
}