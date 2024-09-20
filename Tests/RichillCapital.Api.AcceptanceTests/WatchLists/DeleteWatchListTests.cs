namespace RichillCapital.Api.AcceptanceTests.WatchLists;

public sealed class DeleteWatchListTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_DeleteWatchList()
    {
        var watchListId = "1";

        var response = await Client.DeleteAsync($"api/v1/watch-lists/{watchListId}");

        response.EnsureSuccessStatusCode();
    }
}