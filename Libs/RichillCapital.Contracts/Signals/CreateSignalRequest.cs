namespace RichillCapital.Contracts.Signals;

public sealed record CreateSignalRequest
{
    public required string SourceId { get; init; }
    public required DateTimeOffset CurrentTime { get; init; }
}

public sealed record CreateSignalResponse
{
    public required string Id { get; init; }
}