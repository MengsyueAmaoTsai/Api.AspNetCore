using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Executions.Queries;

public sealed record GetExecutionQuery : IQuery<ErrorOr<ExecutionDto>>
{
    public required string ExecutionId { get; init; }
}
