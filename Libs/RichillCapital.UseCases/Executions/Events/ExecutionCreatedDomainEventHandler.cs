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
            "[ExecutionCreated] {tradeType} {quantity} {symbol} @ {price} {orderType} {timeInForce} for execution id: {executionId}",
            domainEvent.TradeType,
            domainEvent.Quantity,
            domainEvent.Symbol,
            domainEvent.Price,
            domainEvent.OrderType,
            domainEvent.TimeInForce,
            domainEvent.ExecutionId);

        return Task.CompletedTask;
    }
}