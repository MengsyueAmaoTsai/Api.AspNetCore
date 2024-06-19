using RichillCapital.Domain.Common.Repositories;
using RichillCapital.Domain.Users;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Users.Get;

internal sealed class GetUserByIdQueryHandler(
    IReadOnlyRepository<User> _userRepository) :
    IQueryHandler<GetUserByIdQuery, ErrorOr<UserDto>>
{
    public async Task<ErrorOr<UserDto>> Handle(
        GetUserByIdQuery query,
        CancellationToken cancellationToken)
    {
        var idResult = UserId.From(query.UserId);

        if (idResult.IsFailure)
        {
            return idResult.Error
                .ToErrorOr<UserDto>();
        }

        var maybeUser = await _userRepository.GetByIdAsync(idResult.Value, cancellationToken);

        if (maybeUser.IsNull)
        {
            return Error
                .NotFound($"User with id {query.UserId} not found")
                .ToErrorOr<UserDto>();
        }

        return maybeUser.Value
            .ToDto()
            .ToErrorOr();
    }
}