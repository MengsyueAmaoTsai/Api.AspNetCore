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
    Task<Result> SubmitOrderAsync(CancellationToken cancellationToken = default);
}