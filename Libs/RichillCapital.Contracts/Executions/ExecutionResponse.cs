using RichillCapital.UseCases.Executions;

namespace RichillCapital.Contracts.Executions;

public sealed record ExecutionResponse
{
    public required string Id { get; init; }
    public required string Symbol { get; init; }
    public required string TradeType { get; init; }
    public required decimal Quantity { get; init; }
    public required decimal Price { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}

public static class ExecutionResponseMapping
{
    public static ExecutionResponse ToResponse(this ExecutionDto dto) =>
        new()
        {
            Id = dto.Id,
            Symbol = dto.Symbol,
            TradeType = dto.TradeType,
            Quantity = dto.Quantity,
            Price = dto.Price,
            CreatedTimeUtc = dto.CreatedTimeUtc,
        };
}