using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Positions;

namespace RichillCapital.Api.AcceptanceTests.Positions;

public sealed class ListPositionsTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ListPositions()
    {
        var positions = await Client.GetFromJsonAsync<IEnumerable<PositionResponse>>("api/v1/positions");

        positions.Should().NotBeNullOrEmpty();
    }
}