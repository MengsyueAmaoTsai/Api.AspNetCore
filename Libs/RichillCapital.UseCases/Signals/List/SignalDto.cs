namespace RichillCapital.UseCases.Signals;

public class SignalDto
{
    public required string Id { get; init; }

    public required string SourceId { get; init; }

    public required DateTimeOffset Time { get; init; }

    public required string Exchange { get; init; }

    public required string Symbol { get; init; }

    public required decimal Quantity { get; init; }

    public required decimal Price { get; init; }
}
