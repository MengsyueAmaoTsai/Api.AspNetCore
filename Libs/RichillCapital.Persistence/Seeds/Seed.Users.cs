using RichillCapital.Domain;

namespace RichillCapital.Persistence.Seeds;

public static partial class Seed
{
    private const string DefaultPassword = "123";

    internal static IEnumerable<User> CreateUsers()
    {
        yield return CreateUser("UID0000001", "Mengsyue Tsai", "mengsyue.tsai@outlook.com", "+886903776473");
        yield return CreateUser("UID0000002", "Mengsyue Tsai", "mengsyue.tsai@gmail.com", "+886903776473");
    }

    private static User CreateUser(
        string id,
        string name,
        string email,
        string phoneNumber) =>
        User.Create(
            UserId.From(id).Value,
            UserName.From(name).Value,
            Email.From(email).Value,
            PhoneNumber.From(phoneNumber).Value,
            DefaultPassword,
            false,
            false,
            false,
            false,
            0,
            DateTimeOffset.UtcNow).Value;
}