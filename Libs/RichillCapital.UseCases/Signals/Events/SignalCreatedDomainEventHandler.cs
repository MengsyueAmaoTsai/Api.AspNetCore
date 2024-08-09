
using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Signals.Events;

internal sealed class SignalCreatedDomainEventHandler(
    ILogger<SignalCreatedDomainEventHandler> _logger,
    IReadOnlyRepository<Signal> _signalRepository) :
    IDomainEventHandler<SignalCreatedDomainEvent>
{
    public async Task Handle(
        SignalCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        var maybeSignal = await _signalRepository.GetByIdAsync(
            domainEvent.SignalId,
            cancellationToken)
            .ThrowIfNull();

        var signal = maybeSignal.Value;

        _logger.LogInformation(
            "Signal with id {SignalId} created at {Time} for {Symbol}.",
            signal.Id,
            signal.Time,
            signal.Symbol);
    }
}