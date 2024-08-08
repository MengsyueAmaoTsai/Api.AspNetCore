using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class User : Entity<UserId>
{
    private User(UserId id)
        : base(id)
    {
    }

    public static ErrorOr<User> Create(UserId id)
    {
        var user = new User(id);

        return ErrorOr<User>.With(user);
    }
}

public sealed class UserId : SingleValueObject<string>
{
    internal const int MaxLength = 10;

    private UserId(string value) : base(value)
    {
    }

    public static Result<UserId> From(string value) =>
        Result<string>
            .With(value)
            .Then(id => new UserId(id));

}