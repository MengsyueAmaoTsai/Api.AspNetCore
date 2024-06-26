using RichillCapital.Domain.Users;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Persistence.Seeds;

internal static partial class Seed
{
    private const string DefaultPassword = "123";

    internal static IEnumerable<User> CreateUsers()
    {
        yield return CreateUser("UID0000001", "Mengsyue Tsai", "mengsyue.tsai@outlook.com", emailConfirmed: true, lockoutEnabled: false);
        yield return CreateUser("UID0000002", "Mengsyue Tsai", "mengsyue.tsai@gmail.com", emailConfirmed: true, lockoutEnabled: false);
        yield return CreateUser("UID0000003", "Trader Studio Web", "trader-studio-web@gmail.com", emailConfirmed: true, lockoutEnabled: false);
        yield return CreateUser("UID0000004", "Trader Studio Desktop", "trader-studio-desktop@gmail.com", emailConfirmed: true, lockoutEnabled: false);
        yield return CreateUser("UID0000005", "Trader Studio Mobile", "trader-studio-mobile@gmail.com", emailConfirmed: true, lockoutEnabled: false);
        yield return CreateUser("UID0000006", "Research Web", "research-web@gmail.com", emailConfirmed: true, lockoutEnabled: false);
        yield return CreateUser("UID0000007", "Trading Bot Manager Desktop", "trading-bot-manager-desktop@gmail.com", emailConfirmed: true, lockoutEnabled: false);
        yield return CreateUser("UID0000008", "Admin Web", "admin-web@gmail.com", emailConfirmed: true, lockoutEnabled: false);
        yield return CreateUser("UID0000009", "Exchange Web", "exchange-web@gmail.com", emailConfirmed: true, lockoutEnabled: false);
        yield return CreateUser("UID0000010", "Community Web", "community-web@gmail.com", emailConfirmed: true, lockoutEnabled: false);
        yield return CreateUser("UID0000011", "Copy Trader Desktop", "copy-trader-desktop@gmail.com", emailConfirmed: false, lockoutEnabled: true);
    }

    internal static User CreateUser(
        string userId,
        string name,
        string email,
        bool emailConfirmed,
        bool lockoutEnabled) => User.Create(
            id: UserId.From(userId).ThrowIfFailure().Value,
            name: UserName.From(name).ThrowIfFailure().Value,
            email: Email.From(email).ThrowIfFailure().Value,
            emailConfirmed: emailConfirmed,
            passwordHash: DefaultPassword,
            lockoutEnabled: lockoutEnabled,
            accessFailedCount: 0,
            createdAt: DateTimeOffset.UtcNow).Value;
}