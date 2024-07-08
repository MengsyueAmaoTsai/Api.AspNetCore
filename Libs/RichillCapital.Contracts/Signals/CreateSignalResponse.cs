using RichillCapital.Domain;

namespace RichillCapital.Contracts.Signals;

public sealed record CreateSignalResponse
{
    public required Guid Id { get; init; }
}

public static class CreateSignalResponseMapping
{
    public static CreateSignalResponse ToResponse(this SignalId id) =>
        new()
        {
            Id = id.Value,
        };
}