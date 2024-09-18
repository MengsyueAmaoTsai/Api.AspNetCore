using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Events;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Executions.Events;

internal sealed class ExecutionCreatedDomainEventHandler(
    ILogger<ExecutionCreatedDomainEventHandler> _logger) :
    IDomainEventHandler<ExecutionCreatedDomainEvent>
{
    public Task Handle(
        ExecutionCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "EXECUTION CREATED: {tradeType} {quantity} {symbol} @ {price} {orderType} {timeInForce}",
            domainEvent.TradeType,
            domainEvent.Quantity,
            domainEvent.Symbol,
            domainEvent.Price,
            domainEvent.OrderType,
            domainEvent.TimeInForce);

        return Task.CompletedTask;
    }
}