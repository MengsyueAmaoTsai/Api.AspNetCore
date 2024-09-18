using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Orders.Events;

internal sealed class OrderCreatedDomainEventHandler(
    ILogger<OrderCreatedDomainEventHandler> _logger,
    IRepository<Order> _orderRepository,
    IUnitOfWork _unitOfWork) :
    IDomainEventHandler<OrderCreatedDomainEvent>
{
    public async Task Handle(
        OrderCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        LogEvent(domainEvent);

        var order = (await _orderRepository
            .GetByIdAsync(domainEvent.OrderId, cancellationToken)
            .ThrowIfNull())
            .Value;

        var evaluationResult = EvaluateOrderPlacement(order);

        if (evaluationResult.IsFailure)
        {
            var rejectResult = order.Reject(evaluationResult.Error.Message);

            if (rejectResult.IsFailure)
            {
                _logger.LogWarning("Failed to reject order {orderId}: {error}",
                    domainEvent.OrderId,
                    rejectResult.Error);

                return;
            }
        }
        else
        {
            var acceptResult = order.Accept();

            if (acceptResult.IsFailure)
            {
                _logger.LogWarning("Failed to accept order {orderId}: {error}",
                    domainEvent.OrderId,
                    acceptResult.Error);

                return;
            }
        }

        _orderRepository.Update(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private void LogEvent(OrderCreatedDomainEvent @event) =>
        _logger.LogInformation(
            "[OrderCreated] {tradeType} {quantity} {symbol} @ {price} {orderType} {timeInForce} for order id: {orderId}",
            @event.TradeType,
            @event.Quantity,
            @event.Symbol,
            @event.OrderType,
            @event.OrderType,
            @event.TimeInForce,
            @event.OrderId);

    private Result EvaluateOrderPlacement(Order order)
    {
        return Result.Success;
    }
}