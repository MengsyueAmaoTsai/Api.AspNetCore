using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Abstractions;

public interface ISignalManager
{
    Task<Result> MarkAsDelayedAsync(Signal signal, CancellationToken cancellationToken = default);
    Task<Result> AcceptAsync(Signal signal, CancellationToken cancellationToken = default);
}