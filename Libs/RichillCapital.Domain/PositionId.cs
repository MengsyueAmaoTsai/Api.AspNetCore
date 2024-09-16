using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class PositionId : SingleValueObject<string>
{
    internal const int MaxLength = 36;

    private PositionId(string value)
        : base(value)
    {
    }

    public static Result<PositionId> From(string value) =>
        Result<string>
            .With(value)
            .Then(id => new PositionId(id));

    public static PositionId NewPositionId() =>
        From(Guid.NewGuid().ToString()).Value;
}