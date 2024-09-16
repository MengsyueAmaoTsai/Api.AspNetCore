using RichillCapital.Domain;

namespace RichillCapital.UseCases.Executions;

internal static class ExecutionExtensions
{
    internal static ExecutionDto ToDto(this Execution execution) =>
        new()
        {
            Id = execution.Id.Value,
            OrderId = execution.OrderId.Value,
            Symbol = execution.Symbol.Value,
            TradeType = execution.TradeType.ToString(),
            Quantity = execution.Quantity,
            Price = execution.Price,
            CreatedTimeUtc = execution.CreatedTimeUtc,
        };
}