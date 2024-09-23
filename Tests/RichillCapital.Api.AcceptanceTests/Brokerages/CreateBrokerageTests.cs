using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Brokerages;
using RichillCapital.Domain;

namespace RichillCapital.Api.AcceptanceTests.Brokerages;

public sealed class CreateBrokerageTests(
    EndToEndTestWebApplicationFactory factory) :
    BrokerageTests(factory)
{
    [Fact]
    public async Task When_ConnectionExists_Should_ReturnConflict()
    {
        var request = new CreateBrokerageRequest
        {
            Provider = "Binance",
            Name = ExistingConnectionName,
            Arguments = new Dictionary<string, object>
            {
                { "ApiKey", "<ApiKey>" },
            },
        };

        var response = await Client.PostAsJsonAsync("api/v1/brokerages", request);

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task Should_CreateBrokerage()
    {
        var request = new CreateBrokerageRequest
        {
            Provider = "Binance",
            Name = "New connection name from AcceptanceTests",
            Arguments = new Dictionary<string, object>
            {
                { "ApiKey", "<ApiKey>" },
            },
        };

        var response = await Client.PostAsJsonAsync("api/v1/brokerages", request);

        response.EnsureSuccessStatusCode();

        var brokerage = await response.Content.ReadFromJsonAsync<BrokerageDetailsResponse>();

        brokerage.Should().NotBeNull();
        brokerage!.Provider.Should().Be(request.Provider);
        brokerage!.Name.Should().Be(request.Name);
        brokerage.Status.Should().Be(ConnectionStatus.Stopped.Name);
    }
}