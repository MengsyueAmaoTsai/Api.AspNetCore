using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;
using RichillCapital.UseCases.Orders;

namespace RichillCapital.UseCases.Brokerages.Queries;

internal sealed class ListBrokerageOrdersQueryHandler(
    IBrokerageManager _brokerageManager) :
    IQueryHandler<ListBrokerageOrdersQuery, ErrorOr<IEnumerable<OrderDto>>>
{
    public async Task<ErrorOr<IEnumerable<OrderDto>>> Handle(
        ListBrokerageOrdersQuery query,
        CancellationToken cancellationToken)
    {
        var brokerageResult = _brokerageManager.GetByName(query.ConnectionName);

        if (brokerageResult.IsFailure)
        {
            return ErrorOr<IEnumerable<OrderDto>>.WithError(brokerageResult.Error);
        }

        var brokerage = brokerageResult.Value;

        var ordersResult = await brokerage.ListOrdersAsync(cancellationToken);

        if (ordersResult.IsFailure)
        {
            return ErrorOr<IEnumerable<OrderDto>>.WithError(ordersResult.Error);
        }

        var orders = ordersResult.Value;
        return ErrorOr<IEnumerable<OrderDto>>.With(orders
            .Select(o => o.ToDto())
            .ToList());
    }
}