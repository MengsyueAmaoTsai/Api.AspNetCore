using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.SignalSources;
using RichillCapital.Persistence.Seeds;

namespace RichillCapital.Api.AcceptanceTests.SignalSources;

public sealed class GetSignalSourceTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task When_GivenValidRequest_Should_ReturnSignalSource()
    {
        var expectedSource = Seed.CreateSignalSources().First(x => x.Id.Value == "TV-Long-Task");

        var response = await Client.GetAsync($"api/v1/signal-sources/{expectedSource.Id.Value}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<SignalSourceDetailsResponse>();
        result.Should().NotBeNull();
        result!.Id.Should().Be(expectedSource.Id.Value);
        result!.Name.Should().Be(expectedSource.Name);
        result!.Description.Should().Be(expectedSource.Description);
        result!.Signals.Should().BeEmpty();
    }

}