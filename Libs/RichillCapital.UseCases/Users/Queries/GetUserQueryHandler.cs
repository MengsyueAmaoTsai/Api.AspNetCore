using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

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

        var id = validationResult.Value;

        var maybeUser = await _userRepository.GetByIdAsync(id, cancellationToken);

        if (maybeUser.IsNull)
        {
            return ErrorOr<UserDto>.WithError(UserErrors.NotFound(id));
        }

        var user = maybeUser.Value;

        return ErrorOr<UserDto>.With(user.ToDto());
    }
}