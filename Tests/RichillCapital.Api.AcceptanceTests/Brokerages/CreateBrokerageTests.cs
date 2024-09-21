using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Brokerages;

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
        };

        var response = await Client.PostAsJsonAsync("api/v1/brokerages", request);

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

}