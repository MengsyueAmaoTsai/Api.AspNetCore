using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Brokerages.Queries;

internal sealed class ListBrokeragesQueryHandler :
    IQueryHandler<ListBrokeragesQuery, ErrorOr<IEnumerable<BrokerageDto>>>
{
    public Task<ErrorOr<IEnumerable<BrokerageDto>>> Handle(
        ListBrokeragesQuery query,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(
            ErrorOr<IEnumerable<BrokerageDto>>.With([]));
    }
}