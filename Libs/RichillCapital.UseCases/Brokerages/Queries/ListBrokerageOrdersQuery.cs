using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;
using RichillCapital.UseCases.Orders;

namespace RichillCapital.UseCases.Brokerages.Queries;

public sealed record ListBrokerageOrdersQuery :
    IQuery<ErrorOr<IEnumerable<OrderDto>>>
{
    public required string ConnectionName { get; init; }
}
