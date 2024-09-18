using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Orders.Events;

internal sealed class OrderAcceptedDomainEventHandler(
    ILogger<OrderAcceptedDomainEventHandler> _logger,
    IRepository<Order> _orderRepository,
    IUnitOfWork _unitOfWork) :
    IDomainEventHandler<OrderAcceptedDomainEvent>
{
    public async Task Handle(
        OrderAcceptedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        LogEvent(domainEvent);
    }

    private void LogEvent(OrderAcceptedDomainEvent domainEvent) =>
        _logger.LogInformation(
            "[OrderAccepted] {tradeType} {quantity} {symbol} @ {price} {orderType} {timeInForce} for order id: {orderId}",
            domainEvent.TradeType,
            domainEvent.Quantity,
            domainEvent.Symbol,
            domainEvent.OrderType,
            domainEvent.OrderType,
            domainEvent.TimeInForce,
            domainEvent.OrderId);
}