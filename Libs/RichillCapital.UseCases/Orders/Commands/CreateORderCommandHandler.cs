using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Orders.Commands;

internal sealed class CreateOrderCommandHandler(
    IRepository<Order> _orderRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<CreateOrderCommand, ErrorOr<OrderId>>
{
    public async Task<ErrorOr<OrderId>> Handle(
        CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = Result<(TradeType, Symbol, OrderType, TimeInForce)>.Combine(
            TradeType.FromName(command.TradeType)
                .ToResult(Error.Invalid($"Invalid trade type: {command.TradeType}")),
            Symbol.From(command.Symbol),
            OrderType.FromName(command.OrderType)
                .ToResult(Error.Invalid($"Invalid order type: {command.OrderType}")),
            TimeInForce.FromName(command.TimeInForce)
                .ToResult(Error.Invalid($"Invalid time in force: {command.TimeInForce}")));

        var (tradeType, symbol, orderType, timeInForce) = validationResult.Value;

        var errorOrOrder = Order.Create(
            OrderId.NewOrderId(),
            tradeType,
            symbol,
            orderType,
            timeInForce,
            command.Quantity,
            OrderStatus.New,
            DateTimeOffset.UtcNow);

        if (errorOrOrder.HasError)
        {
            return ErrorOr<OrderId>.WithError(errorOrOrder.Errors);
        }

        var order = errorOrOrder.Value;

        _orderRepository.Add(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ErrorOr<OrderId>.With(order.Id);
    }
}