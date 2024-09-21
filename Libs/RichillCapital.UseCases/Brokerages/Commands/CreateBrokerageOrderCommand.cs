using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Brokerages.Commands;

public sealed record CreateBrokerageOrderCommand :
    ICommand<ErrorOr<string>>
{
    public required string ConnectionName { get; init; }
    public required string Symbol { get; init; }
    public required string TradeType { get; init; }
    public required string OrderType { get; init; }
    public required string TimeInForce { get; init; }
    public required decimal Quantity { get; init; }
}
