using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Events;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Trades.Events;

internal sealed class TradeCreatedDomainEventHandler(
    ILogger<TradeCreatedDomainEventHandler> _logger) :
    IDomainEventHandler<TradeCreatedDomainEvent>
{
    public Task Handle(
        TradeCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("TRADE CREATED: {id}", domainEvent.TradeId);

        return Task.CompletedTask;
    }
}