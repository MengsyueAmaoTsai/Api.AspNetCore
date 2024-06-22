using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Signals;

namespace RichillCapital.Api.AcceptanceTests.Signals;

public sealed class ListSignalsTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ReturnSignals()
    {
        var response = await Client.GetFromJsonAsync<Paged<SignalResponse>>("api/v1/signals");

        response.Should().NotBeNull();
        response!.Items.Should().BeEmpty();
    }
}