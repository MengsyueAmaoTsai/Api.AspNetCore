using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Orders.Queries;

public sealed record ListOrdersQuery :
    IQuery<ErrorOr<IEnumerable<OrderDto>>>
{
}
