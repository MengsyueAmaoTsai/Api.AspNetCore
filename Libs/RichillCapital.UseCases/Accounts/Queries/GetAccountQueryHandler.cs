using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Accounts.Queries;

internal sealed class GetAccountQueryHandler(
    IReadOnlyRepository<Account> _accountRepository) :
    IQueryHandler<GetAccountQuery, ErrorOr<AccountDto>>
{
    public async Task<ErrorOr<AccountDto>> Handle(GetAccountQuery query, CancellationToken cancellationToken)
    {
        var validationResult = AccountId.From(query.AccountId);

        if (validationResult.IsFailure)
        {
            return ErrorOr<AccountDto>.WithError(validationResult.Error);
        }

        var id = validationResult.Value;

        var maybeAccount = await _accountRepository.FirstOrDefaultAsync(
            account => account.Id == id,
            cancellationToken);

        if (maybeAccount.IsNull)
        {
            return ErrorOr<AccountDto>.WithError(AccountErrors.NotFound(id));
        }

        return ErrorOr<AccountDto>.With(maybeAccount.Value.ToDto());
    }
}