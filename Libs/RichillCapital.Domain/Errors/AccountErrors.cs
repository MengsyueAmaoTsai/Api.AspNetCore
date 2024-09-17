using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Errors;

public static class AccountErrors
{
    public static Error NotFound(AccountId id) =>
        Error.NotFound("Accounts.NotFound", $"Account with id {id} not found.");
}