using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.SignalSources;

namespace RichillCapital.Api.AcceptanceTests.SignalSources;

public sealed class GetSignalSourceTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ReturnSignalSource()
    {
        var expectedId = "TV-Long-Task";

        var source = await Client.GetFromJsonAsync<SignalSourceResponse>($"api/v1/signal-sources/{expectedId}");

        source.Should().NotBeNull();
        source!.Id.Should().Be(expectedId);
    }
}