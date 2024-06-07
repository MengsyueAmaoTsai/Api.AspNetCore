using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class UserId : SingleValueObject<string>
{
    public const int MaxLength = 100;

    private UserId(string value)
        : base(value)
    {
    }

    public static Result<UserId> From(string value) => value
        .ToResult()
        .Then(id => new UserId(id));
}