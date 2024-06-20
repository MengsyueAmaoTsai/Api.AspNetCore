using RichillCapital.Domain.Users;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Users.Create;

public sealed record CreateUserCommand : 
    ICommand<ErrorOr<UserId>>
{
    public required string Name { get; init; }

    public required string Email { get; init; }

    public required string Password { get; init; }
}
