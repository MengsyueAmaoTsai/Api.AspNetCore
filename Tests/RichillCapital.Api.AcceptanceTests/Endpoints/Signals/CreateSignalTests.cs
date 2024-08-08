using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Signals;
using RichillCapital.Persistence.Seeds;

namespace RichillCapital.Api.AcceptanceTests.Endpoints.Signals;

public sealed class CreateSignalTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_CreateSignal()
    {
        var source = Seed.CreateSignalSources().First();

        var request = new CreateSignalRequest
        {
            SourceId = "1",
            Time = DateTimeOffset.UtcNow,
        };

        var response = await Client.PostAsJsonAsync("api/v1/signals", request);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadFromJsonAsync<CreateSignalResponse>();
        content.Should().NotBeNull();
        content!.Id.Should().NotBeNullOrEmpty();

        var signal = await Client.GetFromJsonAsync<SignalDetailsResponse>($"api/v1/signals/{content.Id}");
        
        signal.Should().NotBeNull();
        signal!.Id.Should().Be(content.Id);
        signal.Time.Should().Be(request.Time);
    }
}
