using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Signals.Events;

internal sealed class SignalAcceptedDomainEventHandler(
    ILogger<SignalAcceptedDomainEventHandler> _logger,
    IReadOnlyRepository<SignalSource> _signalSourceRepository) :
    IDomainEventHandler<SignalAcceptedDomainEvent>
{
    public async Task Handle(
        SignalAcceptedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "[SignalAccepted] {signalId}",
            domainEvent.SourceId);

        var maybeSource = await _signalSourceRepository.GetByIdAsync(
            domainEvent.SourceId,
            cancellationToken);

        var source = maybeSource.ThrowIfNull().Value;
    }
}