using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Brokerages;

internal sealed class BrokerageCollection() :
    IBrokerageCollection
{
    private readonly Dictionary<Guid, IBrokerage> _brokerages = [];

    public IReadOnlyCollection<IBrokerage> All => _brokerages.Values;

    public Result Add(IBrokerage brokerage)
    {
        if (_brokerages.ContainsKey(brokerage.Id))
        {
            return Result.Failure(Error.Conflict(
                "Brokerages.AlreadyExists",
                $"Brokerage connection with id {brokerage.Id} already exists."));
        }

        _brokerages.Add(brokerage.Id, brokerage);

        return Result.Success;
    }
}
