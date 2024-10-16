using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Brokerages;

namespace RichillCapital.Api.AcceptanceTests.Brokerages;

public sealed class GetBrokerageTests(
    EndToEndTestWebApplicationFactory factory) :
    BrokerageTests(factory)
{
    [Fact]
    public async Task Should_ReturnBrokerage()
    {
        var brokerage = await Client.GetFromJsonAsync<BrokerageDetailsResponse>($"api/v1/brokerages/{ExistingConnectionName}");

        brokerage.Should().NotBeNull();
        brokerage!.Name.Should().Be(ExistingConnectionName);
    }
}