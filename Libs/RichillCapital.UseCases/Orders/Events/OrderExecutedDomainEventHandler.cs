using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Events;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Orders.Events;

internal sealed class OrderExecutedDomainEventHandler(
    ILogger<OrderExecutedDomainEventHandler> _logger,
    IUnitOfWork _unitOfWork) :
    IDomainEventHandler<OrderExecutedDomainEvent>
{
    public async Task Handle(
        OrderExecutedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "ORDER EXECUTED: {tradeType} {executionQuantity} {symbol} @ {executionPrice} {orderType} {timeInForce}",
            domainEvent.TradeType,
            domainEvent.Quantity,
            domainEvent.Symbol,
            domainEvent.Price,
            domainEvent.OrderType,
            domainEvent.TimeInForce);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}