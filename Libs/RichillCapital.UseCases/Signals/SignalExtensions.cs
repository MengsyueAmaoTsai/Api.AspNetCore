using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Events;

namespace RichillCapital.UseCases.Signals;

internal static class SignalExtensions
{
    internal static SignalDto ToDto(this Signal signal) =>
        new()
        {
            Id = signal.Id.Value,
            SourceId = signal.SourceId.Value,
            Origin = signal.Origin.Name,
            Symbol = signal.Symbol.Value,
            Time = signal.Time,
            TradeType = signal.TradeType.Name,
            Quantity = signal.Quantity,
            Latency = signal.Latency,
            Status = signal.Status.Name,
            CreatedTimeUtc = signal.CreatedTimeUtc,
        };

    internal static void LogSignalDomainEvent<T>(
        this ILogger<T> logger,
        SignalDomainEvent domainEvent) =>
        logger.LogInformation(
            "[{EventName}] - Signal {SignalId} from {SourceId} ({Origin}) has been {Status} at {UpdatedTimeUtc}. " +
            "Latency: {Latency}ms. " +
            "Trading info: {Time} {TradeType} {Quantity} {Symbol}",
            domainEvent.GetType().Name,
            domainEvent.SignalId,
            domainEvent.SourceId,
            domainEvent.Origin,
            domainEvent.Status,
            domainEvent.CreatedTimeUtc.ToString("HH:mm:ss.fff dd-MM-yyyy"),
            domainEvent.Latency,
            domainEvent.Time.ToString("HH:mm:ss.fff dd-MM-yyyy"),
            domainEvent.Symbol,
            domainEvent.TradeType,
            domainEvent.Quantity);
}