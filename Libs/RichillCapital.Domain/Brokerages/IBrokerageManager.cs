using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Brokerages;

public interface IBrokerageManager
{
    IReadOnlyCollection<IBrokerage> ListAll();
    Result<IBrokerage> GetByName(string name);
    Task<Result<IBrokerage>> CreateAndStartAsync(
        string provider,
        string name,
        CancellationToken cancellationToken = default);
    Result<IBrokerage> Create(string provider, string name);
    Result Remove(IBrokerage brokerage);
    Task<Result> SubmitOrderAsync(string connectionName, Symbol symbol, TradeType tradeType, OrderType orderType, decimal quantity, string clientOrderId, CancellationToken cancellationToken = default);
}