namespace RichillCapital.Contracts.Signals;

public sealed record CreateSignalRequest
{
    public required DateTimeOffset Time { get; init; }
    public required string Origin { get; init; }
    public required string SourceId { get; init; }
}
