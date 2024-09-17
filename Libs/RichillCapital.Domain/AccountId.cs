using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class AccountId : SingleValueObject<string>
{
    internal const int MaxLength = 36;

    private AccountId(string value) : base(value)
    {
    }

    public static Result<AccountId> From(string value) =>
        Result<string>
            .With(value)
            .Ensure(id => !string.IsNullOrEmpty(id), Error.Invalid($"{nameof(AccountId)} cannot be empty"))
            .Ensure(id => id.Length <= MaxLength, Error.Invalid($"{nameof(AccountId)} cannot be longer than {MaxLength} characters"))
            .Then(id => new AccountId(id));

    public static AccountId NewAccountId() =>
        From(Guid.NewGuid().ToString()).ThrowIfFailure().Value;
}
