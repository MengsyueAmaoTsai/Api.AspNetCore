

using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Orders.Queries;

internal sealed class GetOrderQueryHandler() :
    IQueryHandler<GetOrderQuery, ErrorOr<OrderDto>>
{
    public Task<ErrorOr<OrderDto>> Handle(GetOrderQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}