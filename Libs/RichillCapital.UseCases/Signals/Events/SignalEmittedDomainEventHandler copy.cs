using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Signals.Events;

internal sealed class SignalEmittedDomainEventHandler(
    ILogger<SignalEmittedDomainEventHandler> _logger,
    IReadOnlyRepository<Signal> _signalRepository,
    ICopyTradingService _copyTradingService) :
    IDomainEventHandler<SignalEmittedDomainEvent>
{
    public async Task Handle(
        SignalEmittedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "[SignalEmitted] {signalId}",
            domainEvent.SourceId);

        var signal = (await _signalRepository
            .FirstOrDefaultAsync(s => s.Id == domainEvent.SignalId, cancellationToken)
            .ThrowIfNull())
            .Value;

        var result = await _copyTradingService.ReplicateSignalAsync(signal, cancellationToken);

        if (result.IsFailure)
        {
            _logger.LogError(
                "[SignalEmitted] {signalId} failed to replicate signal: {error}",
                domainEvent.SourceId,
                result.Error);
        }
    }
}