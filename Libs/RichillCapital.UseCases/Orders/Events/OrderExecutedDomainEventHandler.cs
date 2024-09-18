using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Orders.Events;

internal sealed class OrderExecutedDomainEventHandler(
    ILogger<OrderExecutedDomainEventHandler> _logger,
    IRepository<Execution> _executionRepository,
    IUnitOfWork _unitOfWork) :
    IDomainEventHandler<OrderExecutedDomainEvent>
{
    public async Task Handle(
        OrderExecutedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        LogEvent(domainEvent);

        var commission = decimal.Zero;
        var tax = decimal.Zero;

        // Persist execution record
        var execution = Execution
            .Create(
                ExecutionId.NewExecutionId(),
                domainEvent.AccountId,
                domainEvent.OrderId,
                domainEvent.Symbol,
                domainEvent.TradeType,
                domainEvent.OrderType,
                domainEvent.TimeInForce,
                domainEvent.Quantity,
                domainEvent.Price,
                commission,
                tax,
                domainEvent.OccurredTime)
            .ThrowIfError()
            .Value;

        _executionRepository.Add(execution);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private void LogEvent(OrderExecutedDomainEvent domainEvent) =>
        _logger.LogInformation(
            "[OrderExecuted] {tradeType} {quantity} {symbol} @ {price} {orderType} {timeInForce} for order id: {orderId}",
            domainEvent.TradeType,
            domainEvent.Quantity,
            domainEvent.Symbol,
            domainEvent.Price,
            domainEvent.OrderType,
            domainEvent.TimeInForce,
            domainEvent.OrderId);
}