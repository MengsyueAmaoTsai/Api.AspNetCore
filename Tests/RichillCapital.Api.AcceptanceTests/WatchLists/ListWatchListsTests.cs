using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.WatchLists;

namespace RichillCapital.Api.AcceptanceTests.WatchLists;

public sealed class ListWatchListsTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ListWatchLists()
    {
        var lists = await Client.GetFromJsonAsync<IEnumerable<WatchListResponse>>("api/v1/watch-lists");

        lists.Should().NotBeNullOrEmpty();
    }
}