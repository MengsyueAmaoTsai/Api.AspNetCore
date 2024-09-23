using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Abstractions;

public interface ICopyTradingService
{
    Task<Result> ReplicateSignalAsync(Signal signal, CancellationToken cancellationToken = default);
}