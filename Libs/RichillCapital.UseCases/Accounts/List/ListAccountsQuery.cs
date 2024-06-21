using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Accounts.List;

public sealed record ListAccountsQuery :
    IQuery<ErrorOr<PagedDto<AccountDto>>>
{
}
