using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Accounts.List;

internal sealed class ListAccountsQueryHandler(
    IReadOnlyRepository<Account> _accountRepository) :
    IQueryHandler<ListAccountsQuery, ErrorOr<PagedDto<AccountDto>>>
{
    public async Task<ErrorOr<PagedDto<AccountDto>>> Handle(
        ListAccountsQuery query,
        CancellationToken cancellationToken)
    {
        var accounts = await _accountRepository.ListAsync(cancellationToken);

        var pagedDto = new PagedDto<AccountDto>
        {
            Items = accounts.Select(account => account.ToDto()),
            TotalCount = accounts.Count,
            Page = 1,
            PageSize = 0,
        };

        return ErrorOr<PagedDto<AccountDto>>.With(pagedDto);
    }
}