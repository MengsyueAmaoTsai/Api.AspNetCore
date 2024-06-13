namespace RichillCapital.Contracts.SignalSources;

public sealed record CreateSignalSourceResponse
{
    public required string Id { get; init; }
}