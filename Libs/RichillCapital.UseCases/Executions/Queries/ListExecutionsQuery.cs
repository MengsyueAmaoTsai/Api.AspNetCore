using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Executions.Queries;

public sealed record ListExecutionsQuery :
    IQuery<ErrorOr<IEnumerable<ExecutionDto>>>
{
}
