using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Brokerages;

public interface IBrokerageManager
{
    Task<Result<IBrokerage>> StartAsync(string connectionName, CancellationToken cancellationToken = default);
    Task<Result<IBrokerage>> StopAsync(string connectionName, CancellationToken cancellationToken = default);
    IReadOnlyCollection<IBrokerage> ListAll();
    Result<IBrokerage> GetByName(string name);
    Result<IBrokerage> Create(string provider, string name, IReadOnlyDictionary<string, object> arguments);
    Result Remove(IBrokerage brokerage);
    Task<Result> SubmitOrderAsync(string connectionName, Symbol symbol, TradeType tradeType, OrderType orderType, TimeInForce timeInForce, decimal quantity, string clientOrderId, CancellationToken cancellationToken = default);
}