namespace RichillCapital.Contracts.Signals;

public sealed record CreateSignalRequest
{
    public required string TradeType { get; init; }
}

public sealed record CreateSignalResponse
{
    public required string Id { get; init; }
}