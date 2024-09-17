using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public class ExecutionId : SingleValueObject<string>
{
    internal const int MaxLength = 36;

    private ExecutionId(string value)
        : base(value)
    {
    }

    public static Result<ExecutionId> From(string value) =>
        Result<string>
            .With(value)
            .Ensure(id => !string.IsNullOrEmpty(id), Error.Invalid($"{nameof(ExecutionId)} cannot be null or empty."))
            .Ensure(id => id.Length <= MaxLength, Error.Invalid($"{nameof(ExecutionId)} cannot be longer than {MaxLength} characters."))
            .Then(id => new ExecutionId(id));

    public static ExecutionId NewExecutionId() =>
        From(Guid.NewGuid().ToString()).ThrowIfFailure().Value;
}