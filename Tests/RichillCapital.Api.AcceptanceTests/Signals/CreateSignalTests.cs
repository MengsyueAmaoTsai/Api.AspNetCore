using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Signals;

namespace RichillCapital.Api.AcceptanceTests.Signals;
public sealed class CreateSignalTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task When_GivenValidRequest_Should_CreateSignal()
    {
        var response = await Client.PostAsJsonAsync("api/v1/signals", new CreateSignalRequest
        {
            TradeType = "Buy",
        });

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<CreateSignalResponse>();
        result.Should().NotBeNull();
        result!.Id.Should().NotBeNullOrWhiteSpace();
    }
}