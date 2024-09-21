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
        _logger.LogInformation(
            "[SignalCreated] {time} signal of source {sourceId} from origin: {origin}",
            domainEvent.Time,
            domainEvent.SourceId,
            domainEvent.Origin);

        var message = new StringBuilder()
            .AppendLine($"Time: {domainEvent.Time}")
            .AppendLine($"SourceId: {domainEvent.SourceId}")
            .AppendLine($"Origin: {domainEvent.Origin}")
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
            Symbol.From("BINANCE:LTCBTC").Value,
            cancellationToken);

        if (orderResult.IsFailure)
        {
            _logger.LogError("{error}", orderResult.Error);
        }
    }
}