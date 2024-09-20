
using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Executions.Queries;

internal sealed class GetExecutionQueryHandler(
    IReadOnlyRepository<Execution> _executionRepository) :
    IQueryHandler<GetExecutionQuery, ErrorOr<ExecutionDto>>
{
    public async Task<ErrorOr<ExecutionDto>> Handle(
        GetExecutionQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = ExecutionId.From(query.ExecutionId);

        if (validationResult.IsFailure)
        {
            return ErrorOr<ExecutionDto>.WithError(validationResult.Error);
        }

        var executionId = validationResult.Value;

        var maybeExecution = await _executionRepository.GetByIdAsync(executionId, cancellationToken);

        if (maybeExecution.IsNull)
        {
            return ErrorOr<ExecutionDto>.WithError(ExecutionErrors.NotFound(executionId));
        }

        var execution = maybeExecution.Value;

        return ErrorOr<ExecutionDto>.With(execution.ToDto());
    }
}