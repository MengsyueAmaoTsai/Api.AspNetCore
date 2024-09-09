using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Orders.Queries;

internal sealed class GetOrderQueryHandler(
    IReadOnlyRepository<Order> _orderRepository) :
    IQueryHandler<GetOrderQuery, ErrorOr<OrderDto>>
{
    public async Task<ErrorOr<OrderDto>> Handle(
        GetOrderQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = OrderId.From(query.OrderId);

        if (validationResult.IsFailure)
        {
            return ErrorOr<OrderDto>.WithError(validationResult.Error);
        }

        var id = validationResult.Value;

        var maybeOrder = await _orderRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (maybeOrder.IsNull)
        {
            return ErrorOr<OrderDto>.WithError(OrderErrors.NotFound(id));
        }

        var order = maybeOrder.Value;

        return ErrorOr<OrderDto>.With(order.ToDto());
    }
}