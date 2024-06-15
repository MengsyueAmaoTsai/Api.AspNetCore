using RichillCapital.Domain;

namespace RichillCapital.UseCases.Signals;

public sealed record SignalDto
{
    public required string Id { get; init; }

    public required string SourceId { get; init; }

    public DateTimeOffset Time { get; init; }

    public required int Latency { get; init; }
}

internal static class SignalExtensions
{
    public static SignalDto ToDto(this Signal signal) =>
        new()
        {
            Id = signal.Id.Value,
            SourceId = signal.SourceId.Value,
            Time = signal.Time,
            Latency = signal.Latency,
        };
}