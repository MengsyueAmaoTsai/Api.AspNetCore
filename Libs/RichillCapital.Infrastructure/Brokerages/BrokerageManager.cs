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
    public Result<IBrokerage> GetByName(string name) => _brokerages.Get(name);
    public IReadOnlyCollection<IBrokerage> ListAll() => _brokerages.All;

    public Result<IBrokerage> Create(string provider, string name, IReadOnlyDictionary<string, object> arguments)
    {
        var createResult = _factory.CreateBrokerage(provider, name, arguments);

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
        string connectionName,
        Symbol symbol,
        TradeType tradeTye,
        OrderType orderType,
        TimeInForce timeInForce,
        decimal quantity,
        string clientOrderId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(connectionName))
        {
            return Result.Failure(Error.Invalid($"{nameof(connectionName)} must not be empty"));
        }

        if (quantity <= decimal.Zero)
        {
            return Result.Failure(Error.Invalid($"{nameof(quantity)} must be greater than zero"));
        }

        var brokerageResult = _brokerages.Get(connectionName);

        if (brokerageResult.IsFailure)
        {
            return Result.Failure(brokerageResult.Error);
        }

        var brokerage = brokerageResult.Value;

        var submitResult = await brokerage.SubmitOrderAsync(
            symbol,
            tradeTye,
            orderType,
            timeInForce,
            quantity,
            clientOrderId,
            cancellationToken);

        if (submitResult.IsFailure)
        {
            return Result.Failure(submitResult.Error);
        }

        return Result.Success;
    }

    public async Task<Result<IBrokerage>> StartAsync(
        string connectionName,
        CancellationToken cancellationToken = default)
    {
        var brokerageResult = _brokerages.Get(connectionName);

        if (brokerageResult.IsFailure)
        {
            return Result<IBrokerage>.Failure(brokerageResult.Error);
        }

        var brokerage = brokerageResult.Value;

        var startResult = await brokerage.StartAsync(cancellationToken);

        if (startResult.IsFailure)
        {
            return Result<IBrokerage>.Failure(startResult.Error);
        }

        return Result<IBrokerage>.With(brokerage);
    }

    public async Task<Result<IBrokerage>> StopAsync(
        string connectionName,
        CancellationToken cancellationToken = default)
    {
        var brokerageResult = _brokerages.Get(connectionName);

        if (brokerageResult.IsFailure)
        {
            return Result<IBrokerage>.Failure(brokerageResult.Error);
        }

        var brokerage = brokerageResult.Value;

        var stopResult = await brokerage.StopAsync(cancellationToken);

        if (stopResult.IsFailure)
        {
            return Result<IBrokerage>.Failure(stopResult.Error);
        }

        return Result<IBrokerage>.With(brokerage);
    }
}