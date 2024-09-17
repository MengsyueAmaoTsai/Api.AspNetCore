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
        _logger.LogInformation("ORDER ACCEPTED: {orderId}", domainEvent.OrderId);

        var maybeOrder = await _orderRepository
            .FirstOrDefaultAsync(o => o.Id == domainEvent.OrderId, cancellationToken)
            .ThrowIfNull();

        var order = maybeOrder.Value;

        var executionQuantity = order.Quantity;
        var executionPrice = order.TradeType == TradeType.Buy ? 100m : 99m;

        var executionResult = order.Execute(executionQuantity, executionPrice);

        if (executionResult.IsFailure)
        {
            _logger.LogError("Failed to execute order: {error}", executionResult.Error);

            return;
        }

        _orderRepository.Update(order);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}