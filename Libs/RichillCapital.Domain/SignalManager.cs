using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

internal sealed class SignalManager(
    IRepository<Signal> _signalRepository) :
    ISignalManager
{
    public async Task<Result> AcceptAsync(
        Signal signal,
        CancellationToken cancellationToken = default)
    {
        var result = signal.Accept();

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        _signalRepository.Update(signal);
        return await Task.FromResult(Result.Success);
    }

    public async Task<Result> MarkAsDelayedAsync(
        Signal signal,
        CancellationToken cancellationToken = default)
    {
        var result = signal.Delay();

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        _signalRepository.Update(signal);
        return await Task.FromResult(Result.Success);
    }
}
