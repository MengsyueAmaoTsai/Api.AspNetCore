
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Accounts.Queries;

public sealed record ListAccountsQuery :
    IQuery<ErrorOr<IEnumerable<AccountDto>>>
{
}
