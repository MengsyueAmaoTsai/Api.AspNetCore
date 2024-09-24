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
    IMatchingEngine _matchingEngine,
    IUnitOfWork _unitOfWork) :
    IDomainEventHandler<OrderAcceptedDomainEvent>
{
    public async Task Handle(
        OrderAcceptedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogOrderDomainEvent(domainEvent);

        var order = (await _orderRepository
            .GetByIdAsync(domainEvent.OrderId, cancellationToken)
            .ThrowIfNull())
            .Value;

        _matchingEngine.MatchOrder(order);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}