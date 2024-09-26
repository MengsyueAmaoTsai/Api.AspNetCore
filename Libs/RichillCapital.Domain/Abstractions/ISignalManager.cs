using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Abstractions;

public interface ISignalManager
{
    Task<Result> MarkAsDelayedAsync(Signal signal, CancellationToken cancellationToken = default);
    Task<Result> EmitAsync(Signal signal, CancellationToken cancellationToken = default);
    Task<Result> BlockAsync(Signal signal, CancellationToken cancellationToken = default);
}