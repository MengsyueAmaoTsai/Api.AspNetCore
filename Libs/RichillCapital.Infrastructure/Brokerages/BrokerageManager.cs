using RichillCapital.Domain;
using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Brokerages;

internal sealed class BrokerageManager(
    IBrokerageCollection _brokerages,
    BrokerageFactory _factory) :
    IBrokerageManager
{
    public async Task<Result<IBrokerage>> CreateAndStartAsync(
        string provider,
        string name,
        CancellationToken cancellationToken = default)
    {
        var brokerageResult = _factory.CreateBrokerage(provider, name);

        if (brokerageResult.IsFailure)
        {
            return Result<IBrokerage>.Failure(brokerageResult.Error);
        }

        var brokerage = brokerageResult.Value;
        _brokerages.Add(brokerage);

        var startResult = await brokerage.StartAsync(cancellationToken);

        if (startResult.IsFailure)
        {
            return Result<IBrokerage>.Failure(startResult.Error);
        }

        return Result<IBrokerage>.With(brokerage);
    }

    public Result<IBrokerage> GetByName(string name) => _brokerages.Get(name);
    public IReadOnlyCollection<IBrokerage> ListAll() => _brokerages.All;

    public Result<IBrokerage> Create(string provider, string name)
    {
        var createResult = _factory.CreateBrokerage(provider, name);

        if (createResult.IsFailure)
        {
            return Result<IBrokerage>.Failure(createResult.Error);
        }

        var brokerage = createResult.Value;
        var addResult = _brokerages.Add(brokerage);

        if (addResult.IsFailure)
        {
            return Result<IBrokerage>.Failure(addResult.Error);
        }

        return Result<IBrokerage>.With(brokerage);
    }

    public Result Remove(IBrokerage brokerage) => _brokerages.Remove(brokerage.Name);

    public async Task<Result> SubmitOrderAsync(
        Symbol symbol,
        TradeType tradeTye,
        OrderType orderType,
        decimal quantity,
        CancellationToken cancellationToken = default)
    {
        if (quantity <= decimal.Zero)
        {
            return Result.Failure(Error.Invalid("Quantity must be greater than zero"));
        }

        var brokerageResult = _brokerages.Get("RichillCapital.Binance");

        if (brokerageResult.IsFailure)
        {
            return Result.Failure(brokerageResult.Error);
        }

        var brokerage = brokerageResult.Value;

        var submitResult = await brokerage.SubmitOrderAsync(
            symbol,
            tradeTye,
            orderType,
            quantity,
            cancellationToken);

        if (submitResult.IsFailure)
        {
            return Result.Failure(submitResult.Error);
        }

        return Result.Success;
    }
}