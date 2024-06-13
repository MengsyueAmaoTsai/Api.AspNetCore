using RichillCapital.Domain;

namespace RichillCapital.Contracts.SignalSources;

public sealed record CreateSignalSourceResponse
{
    public required string Id { get; init; }
}

public static class CreateSignalSourceResponseMapping
{
    public static CreateSignalSourceResponse ToResponse(this SignalSourceId id) =>
        new()
        {
            Id = id.Value,
        };
}