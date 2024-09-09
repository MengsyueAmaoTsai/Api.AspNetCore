
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Orders.Queries;

internal sealed class ListOrdersQueryHandler :
    IQueryHandler<ListOrdersQuery, ErrorOr<IEnumerable<OrderDto>>>
{
    public Task<ErrorOr<IEnumerable<OrderDto>>> Handle(
        ListOrdersQuery query,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}