using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Users.Get;

public sealed record GetUserByIdQuery :
    IQuery<ErrorOr<UserDto>>
{
    public required string UserId { get; init; }
}
