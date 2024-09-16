using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Executions.Events;

internal sealed class ExecutionCreatedDomainEventHandler(
    ILogger<ExecutionCreatedDomainEventHandler> _logger,
    IRepository<Execution> _executionRepository,
    IRepository<Position> _positionRepository,
    IUnitOfWork _unitOfWork) :
    IDomainEventHandler<ExecutionCreatedDomainEvent>
{
    public async Task Handle(
        ExecutionCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        var maybeExecution = await _executionRepository
            .FirstOrDefaultAsync(
                e => e.Id == domainEvent.ExecutionId,
                cancellationToken)
            .ThrowIfNull();

        var execution = maybeExecution.Value;

        var maybePosition = await _positionRepository
            .FirstOrDefaultAsync(p => p.Symbol == execution.Symbol, cancellationToken);

        if (maybePosition.IsNull)
        {
            var newPosition = Position
                .Create(
                    PositionId.NewPositionId(),
                    execution.Symbol)
                .ThrowIfError()
                .Value;

            _positionRepository.Add(newPosition);
        }
        else
        {
            var positionToUpdate = maybePosition.Value;

            _positionRepository.Update(positionToUpdate);

            _logger.LogInformation(
                "Position with symbol {symbol} updated",
                positionToUpdate.Symbol);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}