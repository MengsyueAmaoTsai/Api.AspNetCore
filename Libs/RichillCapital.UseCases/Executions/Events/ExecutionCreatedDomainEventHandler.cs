using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Executions.Events;

internal sealed class ExecutionCreatedDomainEventHandler(
    ILogger<ExecutionCreatedDomainEventHandler> _logger,
    IReadOnlyRepository<Execution> _executionRepository,
    IRepository<Position> _positionRepository,
    IUnitOfWork _unitOfWork) :
    IDomainEventHandler<ExecutionCreatedDomainEvent>
{
    public async Task Handle(
        ExecutionCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("EXECUTION CREATED: {id}", domainEvent.ExecutionId);

        var maybeExecution = await _executionRepository
            .FirstOrDefaultAsync(
                e => e.Id == domainEvent.ExecutionId,
                cancellationToken)
            .ThrowIfNull();

        var execution = maybeExecution.Value;

        await HandleExecutionUsingFlatToFlat(execution, cancellationToken);
    }

    private async Task HandleExecutionUsingFlatToFlat(
        Execution execution,
        CancellationToken cancellationToken = default)
    {
        if (!await _positionRepository.AnyAsync(
            p => p.AccountId == execution.AccountId && p.Symbol == execution.Symbol,
            cancellationToken))
        {
            var newPosition = Position
                .Create(
                    PositionId.NewPositionId(),
                    execution.AccountId,
                    execution.Symbol,
                    execution.TradeType == TradeType.Buy ? Side.Long : Side.Short,
                    execution.Quantity,
                    execution.Price,
                    execution.Commission,
                    execution.Tax,
                    decimal.Zero,
                    execution.CreatedTimeUtc)
                .ThrowIfError()
                .Value;

            _positionRepository.Add(newPosition);
        }
        else
        {
            var maybePosition = await _positionRepository
                .FirstOrDefaultAsync(
                    p => p.AccountId == execution.AccountId && p.Symbol == execution.Symbol,
                    cancellationToken)
                .ThrowIfNull();

            var position = maybePosition.Value;
            _positionRepository.Update(position);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}