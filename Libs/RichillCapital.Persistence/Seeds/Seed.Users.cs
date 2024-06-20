using RichillCapital.Domain.Users;

namespace RichillCapital.Persistence.Seeds;

public static partial class Seed
{
    private const string DefaultPassword = "123";

    internal static IEnumerable<User> CreateUsers()
    {
        yield return CreateUser("UID0000001", "Mengsyue Tsai", "mengsyue.tsai@outlook.com", emailConfirmed: true, lockoutEnabled: false);
        yield return CreateUser("UID0000002", "Mengsyue Tsai", "mengsyue.tsai@gmail.com", emailConfirmed: true, lockoutEnabled: false);
        yield return CreateUser("UID0000003", "Community User", "community-user@gmail.com", emailConfirmed: true, lockoutEnabled: false);
        yield return CreateUser("UID0000004", "Copy Trader User", "copy-trader-user@gmail.com", emailConfirmed: false, lockoutEnabled: true);
    }

    internal static User CreateUser(
        string userId,
        string name,
        string email,
        bool emailConfirmed,
        bool lockoutEnabled) => User.Create(
            id: UserId.From(userId).Value,
            name: UserName.From(name).Value,
            email: Email.From(email).Value,
            emailConfirmed: emailConfirmed,
            passwordHash: DefaultPassword,
            lockoutEnabled: lockoutEnabled,
            accessFailedCount: 0,
            createdAt: DateTimeOffset.UtcNow).Value;
}