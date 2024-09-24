using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Events;

namespace RichillCapital.Infrastructure.Logging;

internal static class SignalExtensions
{
    internal static void LogSignalDomainEvent<T>(
        this ILogger<T> logger,
        SignalDomainEvent domainEvent) =>
        logger.LogInformation(
            "[{EventName}] - Signal {SignalId} from {SourceId} ({Origin}) has been {Status} at {CreatedTimeUtc}. " +
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