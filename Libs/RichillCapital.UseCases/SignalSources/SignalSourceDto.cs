namespace RichillCapital.UseCases.SignalSources;

public sealed record SignalSourceDto
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Visibility { get; init; }
    public required string Status { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}

public sealed record SignalSourcePerformanceDto
{
}