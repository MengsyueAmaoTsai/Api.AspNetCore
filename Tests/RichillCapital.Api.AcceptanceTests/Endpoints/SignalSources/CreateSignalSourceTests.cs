using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.SignalSources;

namespace RichillCapital.Api.AcceptanceTests.Endpoints.SignalSources;

public sealed class CreateSignalSourceTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_CreateSignalSource()
    {
        var createRequest = new CreateSignalSourceRequest
        {
            Id = Guid.NewGuid().ToString(),
        };

        var response = await Client.PostAsJsonAsync("api/v1/signal-sources", createRequest);
        response.EnsureSuccessStatusCode();
        
        var id = (await response.Content.ReadFromJsonAsync<CreateSignalSourceResponse>())!.Id;
        id.Should().Be(createRequest.Id);

        var signalSource = await Client.GetFromJsonAsync<SignalSourceDetailsResponse>($"api/v1/signal-sources/{id}");
        signalSource!.Id.Should().Be(id);
    }
}
