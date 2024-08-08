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
    internal const string Prefix = "UID";

    private UserId(string value)
        : base(value)
    {
    }

    public static Result<UserId> From(string value) =>
        Result<string>
            .With(value)
            .Ensure(id => !string.IsNullOrWhiteSpace(id), Error.Invalid("User id must not be empty"))
            .Ensure(id => id.Length <= MaxLength, Error.Invalid($"User id must be at most {MaxLength} characters long"))
            .Ensure(id => id.StartsWith(Prefix), Error.Invalid($"User id must start with '{Prefix}'"))
            .Then(id => new UserId(id));
}