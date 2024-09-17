using RichillCapital.UseCases.Executions;

namespace RichillCapital.Contracts.Executions;

public record ExecutionResponse
{
    public required string Id { get; init; }
    public required string AccountId { get; init; }
    public required string OrderId { get; init; }
    public required string Symbol { get; init; }
    public required string TradeType { get; init; }
    public required string OrderType { get; init; }
    public required string TimeInForce { get; init; }
    public required decimal Quantity { get; init; }
    public required decimal Price { get; init; }
    public required decimal Commission { get; init; }
    public required decimal Tax { get; init; }
    public required DateTimeOffset CreatedTimeUtc { get; init; }
}

public sealed record ExecutionDetailsResponse : ExecutionResponse
{
}

public static class ExecutionResponseMapping
{
    public static ExecutionResponse ToResponse(this ExecutionDto dto) =>
        new()
        {
            Id = dto.Id,
            AccountId = dto.AccountId,
            OrderId = dto.OrderId,
            Symbol = dto.Symbol,
            TradeType = dto.TradeType,
            OrderType = dto.OrderType,
            TimeInForce = dto.TimeInForce,
            Quantity = dto.Quantity,
            Price = dto.Price,
            Commission = dto.Commission,
            Tax = dto.Tax,
            CreatedTimeUtc = dto.CreatedTimeUtc,
        };

    public static ExecutionDetailsResponse ToDetailsResponse(this ExecutionDto dto) =>
        new()
        {
            Id = dto.Id,
            AccountId = dto.AccountId,
            OrderId = dto.OrderId,
            Symbol = dto.Symbol,
            TradeType = dto.TradeType,
            OrderType = dto.OrderType,
            TimeInForce = dto.TimeInForce,
            Quantity = dto.Quantity,
            Price = dto.Price,
            Commission = dto.Commission,
            Tax = dto.Tax,
            CreatedTimeUtc = dto.CreatedTimeUtc,
        };
}
