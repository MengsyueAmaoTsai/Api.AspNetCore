
using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Orders.Commands;

internal sealed class CreateORderCommandHandler() :
    ICommandHandler<CreateOrderCommand, ErrorOr<OrderId>>
{
    public Task<ErrorOr<OrderId>> Handle(
        CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}