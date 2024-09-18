using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Events;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Positions.Events;

internal sealed class PositionCreatedDomainEventHandler(
    ILogger<PositionCreatedDomainEventHandler> _logger) :
    IDomainEventHandler<PositionCreatedDomainEvent>
{
    public Task Handle(
        PositionCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        LogEvent(domainEvent);

        return Task.CompletedTask;
    }

    private void LogEvent(PositionCreatedDomainEvent domainEvent) =>
        _logger.LogInformation(
            "[PositionCreated] {side} {symbol} {quantity} @ {averagePrice} for position id: {positionId}",
            domainEvent.Side,
            domainEvent.Symbol,
            domainEvent.Quantity,
            domainEvent.AveragePrice,
            domainEvent.PositionId);
}