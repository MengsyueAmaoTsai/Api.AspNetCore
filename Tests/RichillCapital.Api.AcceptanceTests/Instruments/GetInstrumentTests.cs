using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Instruments;

namespace RichillCapital.Api.AcceptanceTests.Instruments;

public sealed class GetInstrumentTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ReturnInstruments()
    {
        var symbol = "NASDAQ:MSFT";

        var instrument = await Client.GetFromJsonAsync<InstrumentDetailsResponse>($"api/v1/instruments/{symbol}");

        instrument.Should().NotBeNull();
        instrument!.Symbol.Should().Be(symbol);
    }
}