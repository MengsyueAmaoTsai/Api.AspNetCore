using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Errors;

public static class ExecutionErrors
{
    public static Error NotFound(ExecutionId id) =>
        Error.NotFound("Executions.NotFound", $"Execution with id {id} was not found.");
}