using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Brokerages;

public interface IBrokerageManager
{
    IReadOnlyCollection<IBrokerage> ListAll();
    Maybe<IBrokerage> GetByName(string name);
    Task<Result<IBrokerage>> CreateAndStartAsync(
        string provider,
        string name,
        CancellationToken cancellationToken = default);
    Result<IBrokerage> Create(string provider, string name);
}