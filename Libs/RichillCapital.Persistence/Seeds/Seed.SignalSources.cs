using RichillCapital.Domain;

namespace RichillCapital.Persistence.Seeds;

public static partial class Seed
{
    internal static IEnumerable<SignalSource> CreateSignalSources()
    {
        yield return CreateSignalSource("TV-Long-Task", "TV Long Task", "TradingView Long Task Signal Source");
        yield return CreateSignalSource("TV-Short-Task", "TV Short Task", "TradingView Short Task Signal Source");
        yield return CreateSignalSource("MT-Long-Task", "MT Long Task", "MetaTrader Long Task Signal Source");
        yield return CreateSignalSource("MT-Short-Task", "MT Short Task", "MetaTrader Short Task Signal Source");
        yield return CreateSignalSource("CT-Long-Task", "CT Long Task", "CTrader Long Task Signal Source");
        yield return CreateSignalSource("CT-Short-Task", "CT Short Task", "CTrader Short Task Signal Source");
    }

    private static SignalSource CreateSignalSource(
        string id,
        string name,
        string description) =>
        SignalSource.Create(
            SignalSourceId.From(id).Value,
            name,
            description).Value;
}