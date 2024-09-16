using RichillCapital.UseCases.Executions;

namespace RichillCapital.Contracts.Executions;

public record ExecutionResponse
{
    public required string Id { get; init; }
    public required string OrderId { get; init; }
    public required string Symbol { get; init; }
    public required string TradeType { get; init; }
    public required decimal Quantity { get; init; }
    public required decimal Price { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}

public sealed record ExecutionDetailsResponse : ExecutionResponse
{
}

public static class ExecutionResponseMapping
{
    public static ExecutionResponse ToResponse(
        this ExecutionDto dto) =>
        new()
        {
            Id = dto.Id,
            OrderId = dto.OrderId,
            Symbol = dto.Symbol,
            TradeType = dto.TradeType,
            Quantity = dto.Quantity,
            Price = dto.Price,
            CreatedTimeUtc = dto.CreatedTimeUtc,
        };

    public static ExecutionDetailsResponse ToDetailsResponse(
        this ExecutionDto dto) =>
        new()
        {
            Id = dto.Id,
            OrderId = dto.OrderId,
            Symbol = dto.Symbol,
            TradeType = dto.TradeType,
            Quantity = dto.Quantity,
            Price = dto.Price,
            CreatedTimeUtc = dto.CreatedTimeUtc,
        };
}