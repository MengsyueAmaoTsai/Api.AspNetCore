using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Positions.Events;

internal sealed class PositionClosedDomainEventHandler(
    ILogger<PositionClosedDomainEventHandler> _logger,
    IRepository<Position> _positionRepository,
    IRepository<Trade> _tradeRepository,
    IUnitOfWork _unitOfWork) :
    IDomainEventHandler<PositionClosedDomainEvent>
{
    public async Task Handle(
        PositionClosedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Position with id {PositionId} has been closed",
            domainEvent.PositionId.Value);

        var maybePosition = await _positionRepository
            .FirstOrDefaultAsync(p => p.Id == domainEvent.PositionId, cancellationToken)
            .ThrowIfNull();

        var position = maybePosition.Value;

        var trade = Trade
            .Create(
                TradeId.NewTradeId(),
                position.Symbol,
                position.Side)
            .ThrowIfError()
            .Value;

        _tradeRepository.Add(trade);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}