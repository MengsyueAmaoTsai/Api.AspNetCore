using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Brokerages.Commands;

internal sealed class CreateBrokerageOrderCommandHandler(
    ILogger<CreateBrokerageOrderCommandHandler> _logger,
    IBrokerageManager _brokerageManager) :
    ICommandHandler<CreateBrokerageOrderCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(
        CreateBrokerageOrderCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create brokerage order with command: {Command}", command);

        var validationResult = Result<(Symbol, TradeType, OrderType, TimeInForce)>.Combine(
            Symbol.From(command.Symbol),
            TradeType.FromName(command.TradeType, ignoreCase: true).ToResult(Error.Invalid($"Invalid TradeType: {command.TradeType}")),
            OrderType.FromName(command.OrderType, ignoreCase: true).ToResult(Error.Invalid($"Invalid OrderType: {command.OrderType}")),
            TimeInForce.FromName(command.TimeInForce, ignoreCase: true).ToResult(Error.Invalid($"Invalid TimeInForce: {command.TimeInForce}")));

        if (validationResult.IsFailure)
        {
            return ErrorOr<string>.WithError(validationResult.Error);
        }

        var (symbol, tradeType, orderType, timeInForce) = validationResult.Value;

        var submitResult = await _brokerageManager.SubmitOrderAsync(
            command.ConnectionName,
            symbol,
            tradeType,
            orderType,
            timeInForce,
            command.Quantity,
            clientOrderId: Guid.NewGuid().ToString(),
            cancellationToken);

        if (submitResult.IsFailure)
        {
            return ErrorOr<string>.WithError(submitResult.Error);
        }

        return ErrorOr<string>.With(Guid.NewGuid().ToString());
    }
}