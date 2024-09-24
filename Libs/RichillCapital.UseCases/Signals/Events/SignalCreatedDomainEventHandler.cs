using System.Text;

using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;
using RichillCapital.UseCases.Signals;

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
        _logger.LogSignalDomainEvent(domainEvent);


        var message = new StringBuilder()
            .AppendLine($"Time: {domainEvent.Time:yyyy-MM-dd HH:mm:ss.fff}")
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
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return;
        }

        await _signalManager.EmitAsync(signal, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private void LogEvent(SignalCreatedDomainEvent @event) =>
        _logger.LogInformation(
            "[SignalCreated] - Signal {SignalId} from {SourceId} ({Origin}) has been {Status} at {CreatedTimeUtc}. " +
            "Latency: {Latency}ms. " +
            "Trading info: {Time} {TradeType} {Quantity} {Symbol}",
            @event.SignalId,
            @event.SourceId,
            @event.Origin,
            @event.Status,
            @event.CreatedTimeUtc.ToString("yyyy-MM-dd HH:mm:ss.fff"),
            @event.Latency,
            @event.Time.ToString("yyyy-MM-dd HH:mm:ss.fff"),
            @event.Symbol,
            @event.TradeType,
            @event.Quantity);
}