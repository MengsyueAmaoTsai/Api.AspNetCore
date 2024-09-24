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
            "[{EventName}] Execution {ExecutionId} for Account {AccountId} has been Created at {CreatedTimeUtc}. " +
            "{TradeType} {quantity} {symbol} @ {price} {orderType} {timeInForce}. " +
            "Commission: {Commission} Tax: {Tax}.",
            domainEvent.GetType().Name,
            domainEvent.ExecutionId,
            domainEvent.AccountId,
            domainEvent.CreatedTimeUtc.ToString("yyyy-MM-dd HH:mm:ss"),
            domainEvent.TradeType,
            domainEvent.Quantity,
            domainEvent.Symbol,
            domainEvent.Price,
            domainEvent.OrderType,
            domainEvent.TimeInForce,
            domainEvent.Commission,
            domainEvent.Tax);

        return Task.CompletedTask;
    }
}