using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Brokerages;

public abstract class Brokerage(
    string provider,
    string name,
    IReadOnlyDictionary<string, object> arguments) :
    IBrokerage
{
    public string Provider { get; private init; } = provider;
    public string Name { get; private init; } = name;
    public IReadOnlyDictionary<string, object> Arguments { get; private init; } = arguments;
    public ConnectionStatus Status { get; protected set; } = ConnectionStatus.Stopped;
    public DateTimeOffset CreatedTimeUtc { get; private init; } = DateTimeOffset.UtcNow;

    public abstract Task<Result> StartAsync(CancellationToken cancellationToken = default);
    public abstract Task<Result> StopAsync(CancellationToken cancellationToken = default);
    public abstract Task<Result> SubmitOrderAsync(Symbol symbol, TradeType tradeType, OrderType orderType, TimeInForce timeInForce, decimal quantity, string clientOrderId, CancellationToken cancellationToken = default);
    public abstract Task<Result<IReadOnlyCollection<Order>>> ListOrdersAsync(CancellationToken cancellationToken = default);
}