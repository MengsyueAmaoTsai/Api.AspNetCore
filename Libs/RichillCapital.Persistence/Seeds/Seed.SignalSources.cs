using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Persistence.Seeds;

internal static partial class Seed
{
    internal static IEnumerable<SignalSource> CreateSignalSources()
    {
        yield return CreateSignalSource(
            id: "1");

        yield return CreateSignalSource(
            id: "2");

        yield return CreateSignalSource(
            id: "3");

        yield return CreateSignalSource(
            id: "4");

        yield return CreateSignalSource(
            id: "5");
    }

    internal static SignalSource CreateSignalSource(string id) =>
        SignalSource
            .Create(
                SignalSourceId.From(id).ThrowIfFailure().ValueOrDefault)
            .ThrowIfError()
            .ValueOrDefault;

    internal static IEnumerable<Signal> CreateSignals()
    {
        yield return CreateSignal(
            id: "1",
            sourceId: "1",
            time: new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            "TXF-1");

        yield return CreateSignal(
            id: "2",
            sourceId: "1",
            time: new DateTimeOffset(2024, 1, 2, 0, 0, 0, TimeSpan.Zero),
            "TXF-1");
    }

    internal static Signal CreateSignal(
        string id,
        string sourceId,
        DateTimeOffset time,
        string symbol) => Signal
        .Create(
            SignalId.From(id).ThrowIfFailure().ValueOrDefault,
            SignalSourceId.From(sourceId).ThrowIfFailure().ValueOrDefault,
            time,
            Symbol.From(symbol).ThrowIfFailure().ValueOrDefault)
        .ThrowIfError()
        .ValueOrDefault;

    internal static IEnumerable<SignalSourceSubscription> CreateSignalSourceSubscriptions()
    {
        yield return CreateSignalSourceSubscription(
            id: "1",
            userId: "UID0000001",
            sourceId: "1");
    }

    internal static SignalSourceSubscription CreateSignalSourceSubscription(
        string id,
        string userId,
        string sourceId) =>
        SignalSourceSubscription
            .Create(
                SignalSourceSubscriptionId.From(id).ThrowIfFailure().ValueOrDefault,
                UserId.From(userId).ThrowIfFailure().ValueOrDefault,
                SignalSourceId.From(sourceId).ThrowIfFailure().ValueOrDefault)
            .ThrowIfError()
            .ValueOrDefault;
}