using RichillCapital.Domain;

namespace RichillCapital.Contracts.Signals;

public sealed record CreateSignalResponse
{
    public required string Id { get; init; }
}

public static class CreateSignalResponseMapping
{
    public static CreateSignalResponse ToResponse(this SignalId signalId) =>
        new()
        {
            Id = signalId.Value,
        };
}