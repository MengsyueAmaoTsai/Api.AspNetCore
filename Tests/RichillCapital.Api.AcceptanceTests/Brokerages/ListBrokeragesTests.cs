using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Brokerages;

namespace RichillCapital.Api.AcceptanceTests.Brokerages;

public sealed class ListBrokeragesTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ListBrokerages()
    {
        var brokerages = await Client.GetFromJsonAsync<IEnumerable<BrokerageResponse>>("api/v1/brokerages");

        brokerages.Should().NotBeNullOrEmpty();
    }
}