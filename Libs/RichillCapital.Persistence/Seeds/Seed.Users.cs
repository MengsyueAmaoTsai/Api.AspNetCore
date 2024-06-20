using RichillCapital.Domain.Users;

namespace RichillCapital.Persistence.Seeds;

public static partial class Seed
{
    private const string DefaultPassword = "123";

    internal static IEnumerable<User> CreateUsers()
    {
        // yield return CreateUser("UID0000001", "Mengsyue Tsai", "mengsyue.tsai@outlook.com", "+886903776473");
        // yield return CreateUser("UID0000002", "Mengsyue Tsai", "mengsyue.tsai@gmail.com", "+886903776473");
        // yield return CreateUser("UID0000003", "Copy Trader User", "copytrader@gmail.com", "+886903776473");
        // yield return CreateUser("UID0000004", "Community User", "anonymous@gmail.com", "+886903776473");
        return [];
    }
}