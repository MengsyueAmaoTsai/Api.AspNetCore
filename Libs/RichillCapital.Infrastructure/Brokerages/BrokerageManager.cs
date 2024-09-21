
using RichillCapital.Domain.Brokerages;
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

    public Maybe<IBrokerage> GetByName(string name) => _brokerages.Get(name);
    public IReadOnlyCollection<IBrokerage> ListAll() => _brokerages.All;

    public Result<IBrokerage> Create(string provider, string name)
    {
        var maybeBrokerage = _brokerages.Get(name);

        if (maybeBrokerage.HasValue)
        {
            return Result<IBrokerage>.Failure(BrokerageErrors.AlreadyExists(name));
        }

        var brokerageResult = _factory.CreateBrokerage(provider, name);

        if (brokerageResult.IsFailure)
        {
            return Result<IBrokerage>.Failure(brokerageResult.Error);
        }

        var brokerage = brokerageResult.Value;
        _brokerages.Add(brokerage);

        return Result<IBrokerage>.With(brokerage);
    }
}