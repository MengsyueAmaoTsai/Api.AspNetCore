using System.Text;

using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Events;
using RichillCapital.Domain.Specifications;
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

        await SendNotificationAsync(domainEvent, cancellationToken);

        var signal = (await _signalRepository
            .FirstOrDefaultAsync(
                new SignalByIdSpecification(domainEvent.SignalId),
                cancellationToken)
            .ThrowIfNull())
            .Value;

        if (domainEvent.Latency > Signal.MaxLatencyInMilliseconds)
        {
            await _signalManager.MarkAsDelayedAsync(signal);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return;
        }

        var emitResult = await _signalManager.EmitAsync(signal, cancellationToken);

        if (emitResult.IsFailure)
        {
            var blockResult = await _signalManager.BlockAsync(signal, cancellationToken);

            if (blockResult.IsFailure)
            {
                throw new InvalidOperationException(
                    "Failed to block signal after failed to emit signal.");
            }

            _logger.LogWarning(
                "Failed to emit signal, signal was blocked. Error: {error}",
                emitResult.Error.Message);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task SendNotificationAsync(
        SignalCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
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
    }
}