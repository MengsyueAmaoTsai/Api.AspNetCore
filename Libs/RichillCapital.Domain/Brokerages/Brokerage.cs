using Microsoft.Extensions.Logging;

using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Brokerages;

public abstract class Brokerage(
    ILogger<Brokerage> _logger,
    Guid id,
    string name) :
    IBrokerage
{
    public Guid Id { get; private init; } = id;
    public string Name { get; private init; } = name;

    public bool IsConnected { get; private set; }

    public virtual Task<Result> StartAsync(
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Starting brokerage {Name} ({Id})",
            Name,
            Id);
        throw new NotImplementedException();
    }

    public virtual Task<Result> StopAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Stopping brokerage {Name} ({Id})",
            Name,
            Id);

        throw new NotImplementedException();
    }
}