using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Signals;

namespace RichillCapital.Api.AcceptanceTests.Signals;

public sealed class CreateSignalTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task When_GivenValidRequest_Should_CreateSignal()
    {
        var request = new CreateSignalRequest
        {
            SourceId = "source-id",
            Time = DateTimeOffset.UtcNow,
            Exchange = "exchange",
            Symbol = "symbol",
            Quantity = 1,
            Price = 1.0m,
            Candle = new CandleInfo
            {
                Time = DateTimeOffset.UtcNow,
                Open = 1.0m,
                Close = 1.0m,
                High = 1.0m,
                Low = 1.0m,
                Volume = 1.0m,
            }
        };

        var response = await Client.PostAsJsonAsync("api/v1/signals", request);
        response.EnsureSuccessStatusCode();

        var createResult = await response.Content.ReadFromJsonAsync<CreateSignalResponse>();
        createResult.Should().NotBeNull();
        createResult!.Id.Should().NotBeEmpty();

        // Verify that the signal was created   
        var signals = await Client.GetFromJsonAsync<Paged<SignalResponse>>("api/v1/signals");
        signals.Should().NotBeNull();
        signals!.Items.Should().NotBeEmpty().And.HaveCount(1);
    }
}