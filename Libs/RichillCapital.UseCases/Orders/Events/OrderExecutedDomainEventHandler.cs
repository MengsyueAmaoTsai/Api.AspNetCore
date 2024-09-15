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
        _logger.LogInformation(
            "Order with id {OrderId} has been executed",
            domainEvent.OrderId);

        var execution = Execution
            .Create(
                ExecutionId.NewExecutionId(),
                domainEvent.Symbol,
                domainEvent.TradeType,
                domainEvent.Quantity,
                domainEvent.Price,
                domainEvent.OccurredTime)
            .ThrowIfError()
            .Value;

        _executionRepository.Add(execution);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}