using RichillCapital.Domain.Common.Repositories;
using RichillCapital.Domain.Users;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Users.Create;

internal sealed class CreateUserCommandHandler(
    IRepository<User> _userRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<CreateUserCommand, ErrorOr<UserId>>
{
    public async Task<ErrorOr<UserId>> Handle(
        CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = Result<(UserName, Email)>.Combine(
            UserName.From(command.Name),
            Email.From(command.Email));
        
        if (validationResult.IsFailure)
        {
            return ErrorOr<UserId>.WithError(validationResult.Error);
        }

        var (name, email) = validationResult.Value;

        if (await _userRepository.AnyAsync(user => user.Email == email, cancellationToken))
        {
            return ErrorOr<UserId>.WithError(Error.Conflict("Users.DuplicateEmail", $""));
        }

        var errorOrUser = User.Create(
            id: UserId.NewUserId(),
            name: name,
            email: email,
            emailConfirmed: false,
            passwordHash: command.Password,
            lockoutEnabled: true,
            accessFailedCount: 0,
            createdAt: DateTimeOffset.UtcNow);

        if (errorOrUser.HasError)
        {
            return ErrorOr<UserId>.WithError(errorOrUser.Errors);
        }

        var user = errorOrUser.Value;

        _userRepository.Add(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ErrorOr<UserId>.With(user.Id);
    }
}
