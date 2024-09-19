using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Positions.Events;

public sealed class PositionClosedDomainEventHandler(
    ILogger<PositionClosedDomainEventHandler> _logger,
    IReadOnlyRepository<Position> _positionRepository,
    IRepository<Trade> _tradeRepository,
    IUnitOfWork _unitOfWork) :
    IDomainEventHandler<PositionClosedDomainEvent>
{
    public async Task Handle(
        PositionClosedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("[PositionClosed] {side} {symbol} {quantity} @ {averagePrice}",
            domainEvent.Side,
            domainEvent.Symbol,
            domainEvent.Quantity,
            domainEvent.AveragePrice);

        var maybePosition = await _positionRepository
            .FirstOrDefaultAsync(p => p.Id == domainEvent.PositionId, cancellationToken)
            .ThrowIfNull();

        var position = maybePosition.Value;

        var oppositeExecutions = position.Executions
            .Where(e => e.TradeType.Reverse().ToSide() == domainEvent.Side)
            .ToList();

        var sameSideExecutions = position.Executions
            .Where(e => e.TradeType.ToSide() == domainEvent.Side)
            .ToList();

        var totalEntryQuantity = sameSideExecutions.Sum(e => e.Quantity);
        var totalExitQuantity = oppositeExecutions.Sum(e => e.Quantity);
        var averageEntryPrice = sameSideExecutions.Sum(e => e.Quantity * e.Price) / totalEntryQuantity;
        var averageExitPrice = oppositeExecutions.Sum(e => e.Quantity * e.Price) / totalExitQuantity;
        var entryTime = sameSideExecutions.Min(e => e.CreatedTimeUtc);
        var exitTime = oppositeExecutions.Max(e => e.CreatedTimeUtc);

        var pointValue = 1;
        var grossProfit = domainEvent.Side == Side.Long ?
            (domainEvent.AveragePrice - averageEntryPrice) * domainEvent.Quantity * pointValue :
            (averageExitPrice - domainEvent.AveragePrice) * domainEvent.Quantity * pointValue;

        var trade = Trade
            .Create(
                TradeId.NewTradeId(),
                position.AccountId,
                position.Symbol,
                position.Side,
                totalEntryQuantity,
                averageEntryPrice,
                entryTime,
                averageExitPrice,
                exitTime,
                position.Commission,
                position.Tax,
                position.Swap,
                grossProfit)
            .ThrowIfError()
            .Then(_tradeRepository.Add)
            .Value;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}