using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.DataFeeds;

namespace RichillCapital.Api.AcceptanceTests.DataFeeds;

public sealed class GetDataFeedTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ListDataFeeds()
    {
        var connectionName = "RichillCapital.Max";

        var dataFeed = await Client.GetFromJsonAsync<DataFeedDetailsResponse>($"api/v1/data-feeds/{connectionName}");

        dataFeed.Should().NotBeNull();
        dataFeed!.Name.Should().Be(connectionName);
    }
}