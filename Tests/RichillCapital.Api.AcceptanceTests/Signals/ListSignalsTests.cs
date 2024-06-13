using System.Net;
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
    public async Task When_RequestIsValid_Should_ReturnSignals()
    {
        var response = await Client.GetAsync("api/v1/signals");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<Paged<SignalResponse>>();

        result.Should().NotBeNull();
        result!.Items.Should().BeEmpty();
    }
}