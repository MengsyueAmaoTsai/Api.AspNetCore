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
        _logger.LogInformation("POSITION CLOSED: {symbol} {side} {quantity} @ {averagePrice}",
            domainEvent.Symbol,
            domainEvent.Side,
            domainEvent.Quantity,
            domainEvent.AveragePrice);

        return Task.CompletedTask;
    }
}