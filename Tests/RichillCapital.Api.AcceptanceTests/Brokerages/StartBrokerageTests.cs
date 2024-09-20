using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Api.Endpoints.Brokerages;
using RichillCapital.Contracts.Brokerages;

namespace RichillCapital.Api.AcceptanceTests.Brokerages;

public sealed class StartBrokerageTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_StartBrokerage()
    {
        var response = await Client.PostAsJsonAsync("api/v1/brokerages/start", new StartBrokerageRequest
        {
            Provider = "RichillCapital",
            Name = "RichillCapital Brokerage Connection"
        });

        response.EnsureSuccessStatusCode();

        var startedBrokerage = await response.Content.ReadFromJsonAsync<BrokerageResponse>();

        startedBrokerage.Should().NotBeNull();
        startedBrokerage!.IsConnected.Should().BeTrue();
    }
}