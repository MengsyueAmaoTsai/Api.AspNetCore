using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class PositionId : SingleValueObject<string>
{
    internal const int MaxLength = 36;

    private PositionId(string value) : base(value)
    {
    }

    public static Result<PositionId> From(string value) =>
        Result<string>
            .With(value)
            .Ensure(id => !string.IsNullOrEmpty(id), Error.Invalid($"{nameof(PositionId)} cannot be null or empty."))
            .Ensure(id => id.Length <= MaxLength, Error.Invalid($"{nameof(PositionId)} cannot be longer than {MaxLength} characters."))
            .Then(id => new PositionId(id));

    public static PositionId NewPositionId() =>
        From(Guid.NewGuid().ToString()).ThrowIfFailure().Value;
}