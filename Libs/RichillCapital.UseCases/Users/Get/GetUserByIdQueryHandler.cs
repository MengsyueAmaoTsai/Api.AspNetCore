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
        var validationResult = UserId.From(query.UserId);

        if (validationResult.IsFailure)
        {
            return ErrorOr<UserDto>
                .WithError(validationResult.Error);
        }

        var userId = validationResult.Value;

        var maybeUser = await _userRepository.GetByIdAsync(userId, cancellationToken);

        if (maybeUser.IsNull)
        {
            return ErrorOr<UserDto>
                .WithError(Error.NotFound("Users.NotFound", $"User with id '{userId}' was not found"));
        }

        var user = maybeUser.Value;

        return ErrorOr<UserDto>
            .With(user.ToDto());
    }
}
