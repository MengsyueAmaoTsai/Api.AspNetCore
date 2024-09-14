using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Execution : Entity<ExecutionId>
{
    private Execution(
        ExecutionId id)
        : base(id)
    {
    }

    public static ErrorOr<Execution> Create(
        ExecutionId id)
    {
        var execution = new Execution(id);

        return ErrorOr<Execution>.With(execution);
    }
}
