using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Brokerages;

internal sealed class BrokerageCollection() :
    IBrokerageCollection
{
    private readonly Dictionary<string, IBrokerage> _brokerages = [];

    public IReadOnlyCollection<IBrokerage> All => _brokerages.Values;

    public Result Add(IBrokerage brokerage)
    {
        if (_brokerages.ContainsKey(brokerage.Name))
        {
            return Result.Failure(BrokerageErrors.AlreadyExists(brokerage.Name));
        }

        _brokerages.Add(brokerage.Name, brokerage);

        return Result.Success;
    }

    public Result<IBrokerage> Get(string name)
    {
        if (!_brokerages.ContainsKey(name))
        {
            return Result<IBrokerage>.Failure(BrokerageErrors.NotFound(name));
        }

        return Result<IBrokerage>.With(_brokerages[name]);
    }

    public Result Remove(string name)
    {
        if (!_brokerages.ContainsKey(name))
        {
            return Result.Failure(BrokerageErrors.NotFound(name));
        }

        _brokerages.Remove(name);

        return Result.Success;
    }
}
