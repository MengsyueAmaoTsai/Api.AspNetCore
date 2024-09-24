using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Accounts.Commands;

internal sealed class CreateAccountCommandHandler(
    IDateTimeProvider _dateTimeProvider,
    IRepository<Account> _accountRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<CreateAccountCommand, ErrorOr<AccountId>>
{
    public async Task<ErrorOr<AccountId>> Handle(
        CreateAccountCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = Result<(UserId, Currency)>.Combine(
            UserId.From(command.UserId),
            Currency.FromName(command.Currency)
                .ToResult(Error.Invalid($"'{nameof(command.Currency)}' is invalid.")));

        if (validationResult.IsFailure)
        {
            return ErrorOr<AccountId>.WithError(validationResult.Error);
        }

        var (userId, currency) = validationResult.Value;

        var errorOrAccount = Account.Create(
            AccountId.NewAccountId(),
            userId,
            command.ConnectionName,
            command.Alias,
            currency,
            _dateTimeProvider.UtcNow);

        if (errorOrAccount.HasError)
        {
            return ErrorOr<AccountId>.WithError(errorOrAccount.Errors);
        }

        var account = errorOrAccount.Value;

        _accountRepository.Add(account);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return ErrorOr<AccountId>.With(account.Id);
    }
}