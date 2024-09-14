using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class ExecutionId : SingleValueObject<string>
{
    internal const int MaxLength = 36;

    private ExecutionId(string value)
        : base(value)
    {
    }

    public static Result<ExecutionId> From(string value) =>
        Result<string>
            .With(value)
            .Then(id => new ExecutionId(id));
}