using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Events;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Positions.Events;

public sealed class PositionClosedDomainEventHandler(
    ILogger<PositionClosedDomainEventHandler> _logger) :
    IDomainEventHandler<PositionClosedDomainEvent>
{
    public Task Handle(
        PositionClosedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("POSITION UPDATED: {side} {symbol} {quantity} @ {averagePrice}",
            domainEvent.Side,
            domainEvent.Symbol,
            domainEvent.Quantity,
            domainEvent.AveragePrice);

        return Task.CompletedTask;
    }
}