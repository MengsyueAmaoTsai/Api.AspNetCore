
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Users.Queries;

public sealed record GetUserQuery : IQuery<ErrorOr<UserDto>>
{
    public required string UserId { get; init; }
}
