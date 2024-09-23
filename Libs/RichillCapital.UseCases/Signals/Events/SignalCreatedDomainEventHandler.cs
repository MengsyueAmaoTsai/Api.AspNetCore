using System.Text;

using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

internal sealed class SignalCreatedDomainEventHandler(
    ILogger<SignalCreatedDomainEventHandler> _logger,
    ILineNotificationService _lineNotification,
    IRepository<Signal> _signalRepository,
    ISignalManager _signalManager,
    IUnitOfWork _unitOfWork) :
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
            .AppendLine($"Quantity: {domainEvent.Quantity}")
            .AppendLine($"Latency: {domainEvent.Latency}")
            .ToString();

        var notifyResult = await _lineNotification.SendAsync(message, cancellationToken);

        if (notifyResult.IsFailure)
        {
            _logger.LogError(
                "Failed to send Line notification: {message}",
                notifyResult.Error.Message);
        }

        var signal = (await _signalRepository
            .GetByIdAsync(domainEvent.SignalId, cancellationToken)
            .ThrowIfNull())
            .Value;

        if (domainEvent.Latency > Signal.MaxLatencyInMilliseconds)
        {
            await _signalManager.MarkAsDelayedAsync(signal);
        }
        else
        {
            await _signalManager.AcceptAsync(signal);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private void LogEvent(SignalCreatedDomainEvent @event) =>
        _logger.LogInformation(
            "[SignalCreated] {tradeType} {quantity} {symbol} for source id: {sourceId} from {origin}. OriginTime: {time} Latency: {latency}",
            @event.TradeType,
            @event.Quantity,
            @event.Symbol,
            @event.SourceId,
            @event.Origin,
            @event.Time,
            @event.Latency);
}