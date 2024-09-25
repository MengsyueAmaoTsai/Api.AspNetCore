using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.SignalSources;
using RichillCapital.Domain;

namespace RichillCapital.Api.AcceptanceTests.SignalSources;

public sealed class CreateSignalSourceTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ReturnSignalSource()
    {
        var request = new CreateSignalSourceRequest
        {
            Id = "test-signal-source",
            Name = "Test Signal Source",
            Description = "Test Signal Source Description",
            Visibility = SignalSourceVisibility.Public.Name,
        };

        var response = await Client.PostAsJsonAsync("api/v1/signal-sources", request);
        response.EnsureSuccessStatusCode();

        var created = await response.Content.ReadFromJsonAsync<SignalSourceCreatedResponse>();

        created.Should().NotBeNull();
        created!.Id.Should().Be(request.Id);

        var signalSource = await Client.GetFromJsonAsync<SignalSourceDetailsResponse>($"api/v1/signal-sources/{created.Id}");

        signalSource.Should().NotBeNull();
        signalSource!.Id.Should().Be(request.Id);
        signalSource.Name.Should().Be(request.Name);
        signalSource.Description.Should().Be(request.Description);
        signalSource.Visibility.Should().Be(request.Visibility);
    }
}