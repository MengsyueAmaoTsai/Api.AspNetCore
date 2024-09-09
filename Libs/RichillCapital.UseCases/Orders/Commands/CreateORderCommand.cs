using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Orders.Commands;

public sealed record CreateOrderCommand :
    ICommand<ErrorOr<OrderId>>
{
}
