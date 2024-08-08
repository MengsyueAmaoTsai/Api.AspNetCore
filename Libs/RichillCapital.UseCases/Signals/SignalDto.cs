using RichillCapital.Domain;

namespace RichillCapital.UseCases.Signals;

public sealed record SignalDto
{
    public required string Id { get; init; }

    public required DateTimeOffset Time { get; init; }
}

internal static class SignalExtensions
{
    internal static SignalDto ToDto(this Signal signal) =>
        new()
        {
            Id = signal.Id.Value,
            Time = signal.Time,
        };
}
