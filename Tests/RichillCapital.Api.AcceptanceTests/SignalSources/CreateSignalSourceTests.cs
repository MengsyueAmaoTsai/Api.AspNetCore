using System.Net;
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
        var response = await Client.PostAsJsonAsync(
            "api/v1/signal-sources",
            new CreateSignalSourceRequest
            {
                Id = "TV-BINANCE:ETHUSDT.P-PL-M15-001",
                Name = "ETHUSDT Strategy",
                Description = "ETHUSDT Strategy",
            });

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<CreateSignalSourceResponse>();
        result.Should().NotBeNull();
        result!.Id.Should().Be("TV-BINANCE:ETHUSDT.P-PL-M15-001");
    }
}