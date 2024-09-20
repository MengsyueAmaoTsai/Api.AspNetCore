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
            return Result.Failure(Error.Conflict(
                "Brokerages.AlreadyExists",
                $"Brokerage with connection name {brokerage.Name} already exists."));
        }

        _brokerages.Add(brokerage.Name, brokerage);

        return Result.Success;
    }
}
