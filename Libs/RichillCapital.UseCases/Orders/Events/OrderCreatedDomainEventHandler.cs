using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel.Diagnostics;
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
        _logger.LogInformation(
            "Order with id {OrderId} has been created",
            domainEvent.OrderId);

        var maybeOrder = await _orderRepository
            .FirstOrDefaultAsync(
                order => order.Id == domainEvent.OrderId,
                cancellationToken)
            .ThrowIfNull();

        var order = maybeOrder.Value;

        if (order.Type == OrderType.Market && order.TimeInForce == TimeInForce.FillOrKill)
        {
            _logger.LogInformation(
                "Order with id {OrderId} is a Market order with FillOrKill TimeInForce",
                domainEvent.OrderId);

            var quotationSnapshot = await GetQuotationSnapshotAsync(cancellationToken);

            var depth = order.TradeType == TradeType.Buy ?
                quotationSnapshot.Ask :
                quotationSnapshot.Bid;

            if (depth.Size < order.Quantity)
            {
                throw new InvalidOperationException(
                    $"Order with id {domainEvent.OrderId} has been rejected because the depth is insufficient");
            }

            var executionResult = order.Execute(order.Quantity, depth.Price);

            // TODO: Handle execution error result

            _orderRepository.Update(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return;
        }
    }

    private async Task<((decimal Price, decimal Size) Bid, (decimal Price, decimal Size) Ask)> GetQuotationSnapshotAsync(
        CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(((100, 1), (101, 1)));
    }
}