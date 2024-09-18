using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Orders.Queries;

internal sealed class ListOrdersQueryHandler(
    IReadOnlyRepository<Order> _orderRepository) :
    IQueryHandler<ListOrdersQuery, ErrorOr<IEnumerable<OrderDto>>>
{
    public async Task<ErrorOr<IEnumerable<OrderDto>>> Handle(
        ListOrdersQuery query,
        CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.ListAsync(cancellationToken);

        return ErrorOr<IEnumerable<OrderDto>>.With(orders
            .Select(o => o.ToDto())
            .ToList());
    }
}