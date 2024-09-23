using RichillCapital.Domain;

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
            OrderType = signal.OrderType.Name,
            Quantity = signal.Quantity,
            CreatedTimeUtc = signal.CreatedTimeUtc,
        };
}