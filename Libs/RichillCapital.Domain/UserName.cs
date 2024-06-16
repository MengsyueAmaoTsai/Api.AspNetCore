using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class UserName : SingleValueObject<string>
{
    public const int MaxLength = 100;

    private UserName(string value)
        : base(value)
    {
    }

    public static Result<UserName> From(string value) => value
        .ToResult()
        .Then(name => new UserName(name));
}
