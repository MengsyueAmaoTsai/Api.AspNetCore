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
        _logger.LogInformation("POSITION CREATED: {side} {symbol} {quantity} @ {averagePrice}",
            domainEvent.Side,
            domainEvent.Symbol,
            domainEvent.Quantity,
            domainEvent.AveragePrice);

        return Task.CompletedTask;
    }
}