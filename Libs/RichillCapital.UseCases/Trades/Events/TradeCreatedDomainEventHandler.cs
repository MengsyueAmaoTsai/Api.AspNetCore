using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Events;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Trades.Events;

internal sealed class TradeCreatedDomainEventHandler(
    ILogger<TradeCreatedDomainEventHandler> _logger) :
    IDomainEventHandler<TradeCreatedDomainEvent>
{
    public Task Handle(
        TradeCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "[{EventName}] - Trade {TradeId} for Account {AccountId} has been created at {ExitTimeUtc}. " +
            "{Side} {Quantity} {Symbol}" +
            "Entry: {EntryPrice} {EntryTimeUtc}. Exit: {ExitPrice} {ExitTimeUtc}. " +
            "Commission: {Commission} Tax: {Tax} Swap: {Swap} P/L: {ProfitLoss}.",
            domainEvent.GetType().Name,
            domainEvent.TradeId,
            domainEvent.AccountId,
            domainEvent.ExitTimeUtc.ToString("yyyy-MM-dd HH:mm:ss"),
            domainEvent.Side,
            domainEvent.Quantity,
            domainEvent.Symbol,
            domainEvent.EntryPrice,
            domainEvent.EntryTimeUtc.ToString("yyyy-MM-dd HH:mm:ss"),
            domainEvent.ExitPrice,
            domainEvent.ExitTimeUtc.ToString("yyyy-MM-dd HH:mm:ss"),
            domainEvent.Commission,
            domainEvent.Tax,
            domainEvent.Swap,
            domainEvent.ProfitLoss);

        return Task.CompletedTask;
    }
}