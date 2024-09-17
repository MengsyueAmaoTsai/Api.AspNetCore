using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class AccountId : SingleValueObject<string>
{
    internal const int MaxLength = 36;

    private AccountId(string value)
        : base(value)
    {
    }

    public static Result<AccountId> From(string value) =>
        Result<string>
            .With(value)
            .Then(id => new AccountId(id));
}