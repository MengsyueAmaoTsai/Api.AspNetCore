using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Users.Queries;

public sealed record ListUsersQuery :
    IQuery<ErrorOr<IEnumerable<UserDto>>>
{
}
