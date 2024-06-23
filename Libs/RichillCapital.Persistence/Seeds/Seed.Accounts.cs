using RichillCapital.Domain;
using RichillCapital.Domain.Users;

namespace RichillCapital.Persistence.Seeds;

internal static partial class Seed
{
    internal static IEnumerable<Account> CreateAccounts()
    {
        yield return CreateAccount("ACC0000001", "UID0000001", "Test account1");
        yield return CreateAccount("ACC0000002", "UID0000001", "Test account2");
        yield return CreateAccount("ACC0000003", "UID0000002", "Test account3");
        yield return CreateAccount("ACC0000004", "UID0000002", "Test account4");
    }

    internal static Account CreateAccount(
        string id,
        string userId,
        string name) =>
        Account.Create(
            AccountId.From(id).Value,
            UserId.From(userId).Value,
            name,
            DateTimeOffset.UtcNow).Value;
}