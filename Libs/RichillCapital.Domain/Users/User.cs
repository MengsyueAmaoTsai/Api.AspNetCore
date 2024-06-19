using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Users;

public sealed class User : Entity<UserId>
{
    private User(
        UserId id,
        UserName name,
        Email email,
        PhoneNumber phoneNumber,
        string password,
        bool lockoutEnabled,
        bool twoFactorEnabled,
        bool emailConfirmed,
        bool phoneNumberConfirmed,
        int accessFailedCount,
        DateTimeOffset lockoutEnd,
        DateTimeOffset createdAt)
        : base(id)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
        LockoutEnabled = lockoutEnabled;
        TwoFactorEnabled = twoFactorEnabled;
        EmailConfirmed = emailConfirmed;
        PhoneNumberConfirmed = phoneNumberConfirmed;
        AccessFailedCount = accessFailedCount;
        LockoutEnd = lockoutEnd;
        CreatedAt = createdAt;
    }

    public UserName Name { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public string Password { get; private set; }
    public bool LockoutEnabled { get; private set; }
    public bool TwoFactorEnabled { get; private set; }
    public bool EmailConfirmed { get; private set; }
    public bool PhoneNumberConfirmed { get; private set; }
    public int AccessFailedCount { get; private set; }
    public DateTimeOffset LockoutEnd { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public static ErrorOr<User> Create(
        UserId id,
        UserName name,
        Email email,
        PhoneNumber phoneNumber,
        string password,
        bool lockoutEnabled,
        bool twoFactorEnabled,
        bool emailConfirmed,
        bool phoneNumberConfirmed,
        int accessFailedCount,
        DateTimeOffset lockoutEnd,
        DateTimeOffset createdAt)
    {
        var newUser = new User(
            id,
            name,
            email,
            phoneNumber,
            password,
            lockoutEnabled,
            twoFactorEnabled,
            emailConfirmed,
            phoneNumberConfirmed,
            accessFailedCount,
            lockoutEnd,
            createdAt);

        return newUser
            .ToErrorOr();
    }
}
