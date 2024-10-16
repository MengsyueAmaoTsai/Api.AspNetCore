using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Orders.Commands;

internal sealed class CreateOrderCommandHandler(
    ILogger<CreateOrderCommandHandler> _logger,
    IDateTimeProvider _dateTimeProvider,
    IReadOnlyRepository<Account> _accountRepository,
    IReadOnlyRepository<Instrument> _instrumentRepository,
    IRepository<Order> _orderRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<CreateOrderCommand, ErrorOr<OrderId>>
{
    public async Task<ErrorOr<OrderId>> Handle(
        CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        // Validate command
        var validationResult = Result<(AccountId, Symbol, TradeType, OrderType, TimeInForce, decimal)>
            .Combine(
                AccountId.From(command.AccountId),
                Symbol.From(command.Symbol),
                TradeType.FromName(command.TradeType)
                    .ToResult(Error.Invalid($"Invalid trade type: {command.TradeType}")),
                OrderType.FromName(command.OrderType)
                    .ToResult(Error.Invalid($"Invalid order type: {command.OrderType}")),
                TimeInForce.FromName(command.TimeInForce)
                    .ToResult(Error.Invalid($"Invalid time in force: {command.TimeInForce}")));

        if (validationResult.IsFailure)
        {
            _logger.LogWarning("Validation failed for CreateOrderCommand: {Errors}", validationResult.Error);

            return ErrorOr<OrderId>.WithError(validationResult.Error);
        }

        // Ensure account exists
        var (accountId, symbol, tradeType, orderType, timeInForce) = validationResult.Value;

        if (!await _accountRepository.AnyAsync(a => a.Id == accountId, cancellationToken))
        {
            _logger.LogWarning("Account with id {AccountId} not found", accountId);

            return ErrorOr<OrderId>.WithError(Error.NotFound($"Account with id {accountId} not found"));
        }

        // Ensure instrument exists
        if (!await _instrumentRepository.AnyAsync(i => i.Symbol == symbol, cancellationToken))
        {
            _logger.LogWarning("Instrument with symbol {Symbol} not found", symbol);

            return ErrorOr<OrderId>.WithError(Error.NotFound($"Instrument with symbol {symbol} not found"));
        }

        // Create and persist order
        var errorOrOrder = Order.Create(
            OrderId.NewOrderId(),
            accountId,
            symbol,
            tradeType,
            orderType,
            timeInForce,
            quantity: command.Quantity,
            remainingQuantity: command.Quantity,
            executedQuantity: decimal.Zero,
            status: OrderStatus.New,
            clientOrderId: string.Empty,
            createdTimeUtc: _dateTimeProvider.UtcNow);

        if (errorOrOrder.HasError)
        {
            _logger.LogWarning("Failed to create order: {Errors}", errorOrOrder.Errors);

            return ErrorOr<OrderId>.WithError(errorOrOrder.Errors);
        }

        var order = errorOrOrder.Value;

        _orderRepository.Add(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ErrorOr<OrderId>.With(order.Id);
    }
}