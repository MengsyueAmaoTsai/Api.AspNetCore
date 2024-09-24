using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;
using RichillCapital.UseCases.Accounts;

namespace RichillCapital.UseCases.Brokerages.Queries;

internal sealed class ListBrokerageAccountsQueryHandler(
    IBrokerageManager _brokerageManager) :
    IQueryHandler<ListBrokerageAccountsQuery, ErrorOr<IEnumerable<AccountDto>>>
{
    public Task<ErrorOr<IEnumerable<AccountDto>>> Handle(
        ListBrokerageAccountsQuery query,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}