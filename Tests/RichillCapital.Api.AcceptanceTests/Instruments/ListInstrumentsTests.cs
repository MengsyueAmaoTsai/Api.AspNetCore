using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Instruments;

namespace RichillCapital.Api.AcceptanceTests.Instruments;

public sealed class ListInstrumentsTests(
    AcceptanceTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ReturnInstruments()
    {
        var instruments = await Client.GetFromJsonAsync<Paged<InstrumentResponse>>("api/v1/instruments");

        instruments.Should().NotBeNull();
        instruments!.Items.Should().NotBeEmpty();
    }
}