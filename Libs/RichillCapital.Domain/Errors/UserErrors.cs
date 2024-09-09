using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Errors;

public static class UserErrors
{
    public static Error NotFound(UserId id) => Error.NotFound("Users.NotFound", $"User with Id {id} not found.");
    public static Error NotFound(Email email) => Error.NotFound("Users.NotFound", $"User with Email {email} not found.");
}