using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Instruments;

namespace RichillCapital.Api.AcceptanceTests.Instruments;

public sealed class ListInstrumentsTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ListInstruments()
    {
        var instruments = await Client.GetFromJsonAsync<IEnumerable<InstrumentResponse>>("api/v1/instruments");

        instruments.Should().NotBeNullOrEmpty();
    }
}