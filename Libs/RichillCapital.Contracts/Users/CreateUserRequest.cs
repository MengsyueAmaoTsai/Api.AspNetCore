using RichillCapital.UseCases.Users.Create;

namespace RichillCapital.Contracts.Users;

public sealed record CreateUserRequest
{
    public required string Name { get; init; }

    public required string Email { get; init; }

    public required string Password { get; init; }
}

public static class CreateUserRequestMapping
{
    public static CreateUserCommand ToCommand(this CreateUserRequest request) =>
        new()
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
        };
}
