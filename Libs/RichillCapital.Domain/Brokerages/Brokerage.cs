using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Brokerages;

public abstract class Brokerage(
    string provider,
    string name) :
    IBrokerage
{
    public string Provider { get; private init; } = provider;
    public string Name { get; private init; } = name;
    public bool IsConnected { get; protected set; }

    public abstract Task<Result> StartAsync(CancellationToken cancellationToken = default);
    public abstract Task<Result> StopAsync(CancellationToken cancellationToken = default);
    public abstract Task<Result> SubmitOrderAsync(
            Symbol symbol,
            TradeType tradeType,
            OrderType orderType,
            decimal quantity,
            string clientOrderId,
            CancellationToken cancellationToken = default);
}