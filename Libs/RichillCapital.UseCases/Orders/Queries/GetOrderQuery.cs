using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Orders.Queries;

public sealed record GetOrderQuery : IQuery<ErrorOr<OrderDto>>
{
    public required string OrderId { get; init; }
}
