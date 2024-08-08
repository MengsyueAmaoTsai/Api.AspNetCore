
using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Users.Queries;

internal sealed class GetUserQueryHandler(
    IReadOnlyRepository<User> _userRepository) :
    IQueryHandler<GetUserQuery, ErrorOr<UserDto>>
{
    public async Task<ErrorOr<UserDto>> Handle(
        GetUserQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = UserId.From(query.UserId);

        if (validationResult.IsFailure)
        {
            return ErrorOr<UserDto>.WithError(validationResult.Error);
        }

        var userId = validationResult.Value;

        var maybeUser = await _userRepository.GetByIdAsync(userId, cancellationToken);

        if (maybeUser.IsNull)
        {
            var error = Error.NotFound("Users.NotFound", $"User with id {userId} not found");
            return ErrorOr<UserDto>.WithError(error);
        }

        var user = maybeUser.Value;

        return ErrorOr<UserDto>.With(user.ToDto());
    }
}