using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.WatchLists;

namespace RichillCapital.Api.AcceptanceTests.WatchLists;

public sealed class GetWatchListTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ReturnWatchList()
    {
        var watchListId = "1";

        var watchList = await Client.GetFromJsonAsync<WatchListDetailsResponse>($"api/v1/watch-lists/{watchListId}");

        watchList.Should().NotBeNull();
        watchList!.Id.Should().Be(watchListId);
    }
}