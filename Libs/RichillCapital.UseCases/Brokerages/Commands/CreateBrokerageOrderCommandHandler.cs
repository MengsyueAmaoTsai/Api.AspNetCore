using RichillCapital.Domain;
using RichillCapital.Domain.Brokerages;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Brokerages.Commands;

internal sealed class CreateBrokerageOrderCommandHandler(
    IBrokerageManager _brokerageManager) :
    ICommandHandler<CreateBrokerageOrderCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(
        CreateBrokerageOrderCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = Result<(Symbol, TradeType, OrderType)>.Combine(
            Symbol.From(command.Symbol),
            TradeType.FromName(command.TradeType, ignoreCase: true).ToResult(Error.Invalid($"Invalid TradeType: {command.TradeType}")),
            OrderType.FromName(command.OrderType, ignoreCase: true).ToResult(Error.Invalid($"Invalid OrderType: {command.OrderType}")));

        if (validationResult.IsFailure)
        {
            return ErrorOr<string>.WithError(validationResult.Error);
        }

        var (symbol, tradeType, orderType) = validationResult.Value;

        var submitResult = await _brokerageManager.SubmitOrderAsync(
            symbol,
            tradeType,
            orderType,
            command.Quantity,
            cancellationToken);

        if (submitResult.IsFailure)
        {
            return ErrorOr<string>.WithError(submitResult.Error);
        }

        return ErrorOr<string>.With(Guid.NewGuid().ToString());
    }
}