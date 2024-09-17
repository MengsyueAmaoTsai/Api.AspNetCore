using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Accounts.Queries;

internal sealed class ListAccountsQueryHandler(
    IReadOnlyRepository<Account> _accountRepository) :
    IQueryHandler<ListAccountsQuery, ErrorOr<IEnumerable<AccountDto>>>
{
    public async Task<ErrorOr<IEnumerable<AccountDto>>> Handle(
        ListAccountsQuery query,
        CancellationToken cancellationToken)
    {
        var accounts = await _accountRepository.ListAsync(cancellationToken);

        var result = accounts
            .Select(a => a.ToDto())
            .ToList();

        return ErrorOr<IEnumerable<AccountDto>>.With(result);
    }
}