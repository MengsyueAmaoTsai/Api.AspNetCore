using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.SignalSources;

namespace RichillCapital.Api.AcceptanceTests.SignalSources;

public sealed class CreateSignalSourceTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task When_GivenValidRequest_Should_CreateSignalSource()
    {
        var request = new CreateSignalSourceRequest
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Test Signal Source",
            Description = "Test Signal Source Description",
        };

        var response = await Client.PostAsJsonAsync("api/v1/signal-sources", request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<CreateSignalSourceResponse>();
        result.Should().NotBeNull();
        result!.Id.Should().Be(request.Id);
    }
}
