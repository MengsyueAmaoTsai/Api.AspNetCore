

using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Executions.Queries;

internal sealed class ListExecutionsQueryHandler(
    IReadOnlyRepository<Execution> _executionRepository) :
    IQueryHandler<ListExecutionsQuery, ErrorOr<IEnumerable<ExecutionDto>>>
{
    public async Task<ErrorOr<IEnumerable<ExecutionDto>>> Handle(ListExecutionsQuery query, CancellationToken cancellationToken)
    {
        var executions = await _executionRepository.ListAsync(cancellationToken);

        var result = executions
            .Select(execution => execution.ToDto())
            .ToList();

        return ErrorOr<IEnumerable<ExecutionDto>>.With(result);
    }
}