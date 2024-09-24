using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;
using RichillCapital.UseCases.Accounts;

namespace RichillCapital.UseCases.Brokerages.Queries;

public sealed record ListBrokerageAccountsQuery :
    IQuery<ErrorOr<IEnumerable<AccountDto>>>
{
    public required string ConnectionName { get; init; }
}
