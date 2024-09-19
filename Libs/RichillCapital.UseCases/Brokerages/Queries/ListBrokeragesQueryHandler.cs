using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Brokerages.Queries;

internal sealed class ListBrokeragesQueryHandler(
    IBrokerageManager _brokerageManager) :
    IQueryHandler<ListBrokeragesQuery, ErrorOr<IEnumerable<BrokerageDto>>>
{
    public Task<ErrorOr<IEnumerable<BrokerageDto>>> Handle(
        ListBrokeragesQuery query,
        CancellationToken cancellationToken)
    {
        var brokerages = _brokerageManager.ListAll();

        return Task.FromResult(
            ErrorOr<IEnumerable<BrokerageDto>>.With(brokerages
                .Select(b => b.ToDto())
                .ToList()));
    }
}