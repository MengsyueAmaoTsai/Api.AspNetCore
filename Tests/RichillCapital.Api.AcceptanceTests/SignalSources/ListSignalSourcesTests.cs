using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts;
using RichillCapital.Contracts.SignalSources;
using RichillCapital.Persistence.Seeds;

namespace RichillCapital.Api.AcceptanceTests.SignalSources;

public sealed class ListSignalSourcesTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task When_GivenValidRequest_Should_ReturnSignalSources()
    {
        var response = await Client.GetAsync("api/v1/signal-sources");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<Paged<SignalSourceResponse>>();
        result.Should().NotBeNull();
        result!.Items.Should().NotBeEmpty().And.HaveCount(Seed.CreateSignalSources().Count());
    }
}