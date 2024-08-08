using RichillCapital.UseCases.Signals;

namespace RichillCapital.Contracts.Signals;

public sealed record SignalDetailsResponse
{
    public required string Id { get; init; }
    public required DateTimeOffset Time { get; init; }
}

public static class SignalDetailsResponseMapping
{
    public static SignalDetailsResponse ToResponse(this SignalDto dto) =>
        new()
        {
            Id = dto.Id,
            Time = dto.Time,
        };
}