using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.DataFeeds;

namespace RichillCapital.Api.AcceptanceTests.DataFeeds;

public sealed class ListDataFeedsTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ListDataFeeds()
    {
        var dataFeeds = await Client.GetFromJsonAsync<IEnumerable<DataFeedResponse>>("api/v1/data-feeds");

        dataFeeds.Should().NotBeNullOrEmpty();
    }
}