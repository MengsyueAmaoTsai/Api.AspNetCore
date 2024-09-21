using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Brokerages;

public interface IBrokerage : IConnection
{
    string Provider { get; }

    Task<Result> SubmitOrderAsync(
        Symbol symbol,
        CancellationToken cancellationToken = default);
}