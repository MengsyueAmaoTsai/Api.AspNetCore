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
    private const decimal CommissionRate = 0.001m;
    private const decimal TaxRate = 0.001m;

    public async Task Handle(
        OrderExecutedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("ORDER EXECUTED: {tradeType} {executionQuantity} {symbol} @ {executionPrice} {orderType} {timeInForce}",
            domainEvent.TradeType,
            domainEvent.Quantity,
            domainEvent.Symbol,
            domainEvent.Price,
            domainEvent.OrderType,
            domainEvent.TimeInForce);

        var executionSize = domainEvent.Quantity * domainEvent.Price;

        var commission = executionSize * CommissionRate;
        var tax = domainEvent.TradeType == TradeType.Buy ?
            decimal.Zero :
            executionSize * TaxRate;

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
}