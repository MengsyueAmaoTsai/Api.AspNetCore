using RichillCapital.Domain;

namespace RichillCapital.Persistence.Seeds;

public static partial class Seed
{
    private const string DefaultPassword = "123";

    private static IEnumerable<User> CreateUsers()
    {
        yield return CreateUser("1", "mengsyue.tsai@outlook.com", "Mengsyue Tsai");
        yield return CreateUser("2", "mengsyue.tsai@gmail.com", "Mengsyue Tsai");
    }

    private static User CreateUser(
        string id,
        string email,
        string name) =>
        User.Create(
            UserId.From(id).Value,
            Email.From(email).Value,
            DefaultPassword,
            name).Value;
}