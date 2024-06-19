
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.Domain.Users;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Users.List;

internal sealed class ListUsersQueryHandler(
    IReadOnlyRepository<User> _userRepository) :
    IQueryHandler<ListUsersQuery, ErrorOr<PagedDto<UserDto>>>
{
    public async Task<ErrorOr<PagedDto<UserDto>>> Handle(
        ListUsersQuery query,
        CancellationToken cancellationToken)
    {
        var users = await _userRepository.ListAsync(cancellationToken);

        return new PagedDto<UserDto>
        {
            Items = users
                .Select(user => user.ToDto()),
        }.ToErrorOr();
    }
}