using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel.Monads;

internal sealed class BinanceBrokerage(
    ILogger<BinanceBrokerage> _logger,
    Guid id,
    string name) :
    Brokerage(_logger, id, name)
{
    public Task<Result> StartAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Result.Success);
    }

    public Task<Result> StopAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Result.Success);
    }
}