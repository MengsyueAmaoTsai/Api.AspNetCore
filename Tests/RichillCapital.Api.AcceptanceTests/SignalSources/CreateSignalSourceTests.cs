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
        // Create a new signal source
        var id = "TV-BINANCE:ETHUSDT.P-PL-M15-001";
        var name = "ETHUSDT Strategy";
        var description = "ETHUSDT Strategy";

        var response = await Client.PostAsJsonAsync(
            "api/v1/signal-sources",
            new CreateSignalSourceRequest
            {
                Id = id,
                Name = name,
                Description = description,
            });

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<CreateSignalSourceResponse>();
        result.Should().NotBeNull();
        result!.Id.Should().Be(id);

        // Get the created signal source
        var getResponse = await Client.GetAsync($"api/v1/signal-sources/{result.Id}");

        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var getResult = await getResponse.Content.ReadFromJsonAsync<SignalSourceDetailsResponse>();

        getResult.Should().NotBeNull();

        getResult!.Id.Should().Be(id);
        getResult!.Name.Should().Be(name);
        getResult!.Description.Should().Be(description);
    }
}