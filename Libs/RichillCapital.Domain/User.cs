using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class User : Entity<UserId>
{
    private User(
        UserId id,
        Email email,
        string password,
        string name)
        : base(id)
    {
        Email = email;
        Password = password;
        Name = name;
    }

    public Email Email { get; private set; }

    public string Password { get; private set; }

    public string Name { get; private set; }

    public static ErrorOr<User> Create(
        UserId id,
        Email email,
        string password,
        string name)
    {
        var newUser = new User(
            id,
            email,
            password,
            name);

        return newUser
            .ToErrorOr();
    }
}
