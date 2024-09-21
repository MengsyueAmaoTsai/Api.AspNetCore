using System.Text;

using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Brokerages;
using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

internal sealed class SignalCreatedDomainEventHandler(
    ILogger<SignalCreatedDomainEventHandler> _logger,
    ILineNotificationService _lineNotification,
    IBrokerageManager _brokerageManager) :
    IDomainEventHandler<SignalCreatedDomainEvent>
{
    public async Task Handle(
        SignalCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        LogEvent(domainEvent);

        var message = new StringBuilder()
            .AppendLine($"Time: {domainEvent.Time}")
            .AppendLine($"SourceId: {domainEvent.SourceId}")
            .AppendLine($"Origin: {domainEvent.Origin}")
            .AppendLine($"Symbol: {domainEvent.Symbol}")
            .AppendLine($"TradeType: {domainEvent.TradeType}")
            .AppendLine($"OrderType: {domainEvent.OrderType}")
            .AppendLine($"Quantity: {domainEvent.Quantity}")
            .ToString();

        var result = await _lineNotification.SendAsync(
            string.Empty,
            message,
            cancellationToken);

        if (result.IsFailure)
        {
            _logger.LogError(
                "Failed to send Line notification: {message}",
                result.Error.Message);
        }

        // TEMP IMPLEMENTATION
        var brokerage = _brokerageManager.GetByName("Binance").ThrowIfNull().Value;

        var orderResult = await brokerage.SubmitOrderAsync(
            domainEvent.Symbol,
            domainEvent.TradeType,
            domainEvent.OrderType,
            domainEvent.Quantity,
            cancellationToken);

        if (orderResult.IsFailure)
        {
            _logger.LogError("{error}", orderResult.Error);
        }
    }

    private void LogEvent(SignalCreatedDomainEvent @event) =>
        _logger.LogInformation(
            "[SignalCreated] {tradeType} {quantity} {symbol} @ {price} {orderType} for source id: {sourceId} from {origin}. {time}",
            @event.TradeType,
            @event.Quantity,
            @event.Symbol,
            @event.OrderType,
            @event.OrderType,
            @event.SourceId,
            @event.Origin,
            @event.Time);
}