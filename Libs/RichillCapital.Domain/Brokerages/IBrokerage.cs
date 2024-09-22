using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Brokerages;

public interface IBrokerage : IConnection
{
    string Provider { get; }
    DateTimeOffset CreatedTimeUtc { get; }

    Task<Result> SubmitOrderAsync(
            Symbol symbol,
            TradeType tradeType,
            OrderType orderType,
            TimeInForce timeInForce,
            decimal quantity,
            string clientOrderId,
            CancellationToken cancellationToken = default);

    Task<Result<IReadOnlyCollection<Order>>> ListOrdersAsync(CancellationToken cancellationToken = default);
}