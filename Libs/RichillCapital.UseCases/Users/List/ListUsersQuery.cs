using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Users.List;

public sealed record ListUsersQuery :
    IQuery<ErrorOr<PagedDto<UserDto>>>
{
}
