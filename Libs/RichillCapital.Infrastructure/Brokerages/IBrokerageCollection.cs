using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Brokerages;

public interface IBrokerageCollection
{
    IReadOnlyCollection<IBrokerage> All { get; }
    Result Add(IBrokerage brokerage);
    Result<IBrokerage> Get(string name);
    Result Remove(string name);
}
