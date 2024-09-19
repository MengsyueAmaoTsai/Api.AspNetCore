using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.WatchLists;

namespace RichillCapital.Api.AcceptanceTests.WatchLists;

public sealed class CreateWatchListTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_CreateWatchList()
    {
        var request = new CreateWatchListRequest
        {
            UserId = "1",
            Name = "Watch list 2",
        };

        var response = await Client.PostAsJsonAsync("api/v1/watch-lists", request);
        response.EnsureSuccessStatusCode();

        var created = await response.Content.ReadFromJsonAsync<WatchListCreatedResponse>();

        created.Should().NotBeNull();
        created!.Id.Should().NotBeNullOrWhiteSpace();
    }
}