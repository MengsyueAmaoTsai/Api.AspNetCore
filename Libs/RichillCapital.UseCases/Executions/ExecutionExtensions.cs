using RichillCapital.Domain;

namespace RichillCapital.UseCases.Executions;

internal static class ExecutionExtensions
{
    internal static ExecutionDto ToDto(this Execution execution) =>
        new()
        {
            Id = execution.Id.Value,
            AccountId = execution.AccountId.Value,
            OrderId = execution.OrderId.Value,
            PositionId = execution.PositionId.Value,
            Symbol = execution.Symbol.Value,
            TradeType = execution.TradeType.Name,
            OrderType = execution.OrderType.Name,
            TimeInForce = execution.TimeInForce.Name,
            Quantity = execution.Quantity,
            Price = execution.Price,
            Commission = execution.Commission,
            Tax = execution.Tax,
            CreatedTimeUtc = execution.CreatedTimeUtc,
        };
}