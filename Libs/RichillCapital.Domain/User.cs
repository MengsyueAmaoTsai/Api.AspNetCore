using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class User : Entity<UserId>
{
    public User(
        UserId id,
        string name,
        Email email,
        bool emailConfirmed,
        string passwordHash,
        DateTimeOffset createdTimeUtc)
        : base(id)
    {
        Name = name;
        Email = email;
        EmailConfirmed = emailConfirmed;
        PasswordHash = passwordHash;
        CreatedTimeUtc = createdTimeUtc;
    }

    public string Name { get; private set; }
    public Email Email { get; private set; }
    public bool EmailConfirmed { get; private set; }
    public string PasswordHash { get; private set; }
    public DateTimeOffset CreatedTimeUtc { get; private set; }

    public static ErrorOr<User> Create(
        UserId id,
        string name,
        Email email,
        bool emailConfirmed,
        string passwordHash)
    {
        var user = new User(
            id,
            name,
            email,
            emailConfirmed,
            passwordHash,
            DateTimeOffset.UtcNow);

        return ErrorOr<User>.With(user);
    }
}
