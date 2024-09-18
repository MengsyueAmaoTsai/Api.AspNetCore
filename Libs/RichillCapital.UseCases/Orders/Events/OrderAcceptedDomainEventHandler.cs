using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel;
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

        _logger.LogInformation("Handling order type: {type}, tif: {tif}", order.Type, order.TimeInForce);

        var result = order switch
        {
            { Type.Name: nameof(OrderType.Market), TimeInForce.Name: nameof(TimeInForce.ImmediateOrCancel) } =>
                await HandleMarketOrderIocAsync(order, cancellationToken),

            _ => Result.Failure(Error.Invalid($"Unsupported order type or time in force: {order.Type}")),
        };
    }

    private async Task<Result> HandleMarketOrderIocAsync(
        Order order,
        CancellationToken cancellationToken = default)
    {
        var executionQuantity = order.Quantity;
        var executionPrice = order.TradeType == TradeType.Buy ? 100m : 99m;

        var executionResult = order.Execute(executionQuantity, executionPrice);

        if (executionResult.IsFailure)
        {
            _logger.LogError("Failed to execute order: {error}", executionResult.Error);

            return Result.Failure(executionResult.Error);
        }

        _orderRepository.Update(order);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}