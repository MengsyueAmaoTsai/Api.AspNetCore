using System.Net.Http.Json;

using FluentAssertions;

using RichillCapital.Contracts.Positions;

namespace RichillCapital.Api.AcceptanceTests.Positions;

public sealed class PositionTests(
    EndToEndTestWebApplicationFactory factory) :
    AcceptanceTest(factory)
{
    [Fact]
    public async Task Should_ReturnPosition()
    {
        var positionId = "1";

        var position = await Client.GetFromJsonAsync<PositionDetailsResponse>($"api/v1/positions/{positionId}");

        position.Should().NotBeNull();
        position!.Id.Should().Be(positionId);
    }
}