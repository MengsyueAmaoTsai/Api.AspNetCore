using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Persistence.Seeds;

internal static partial class Seed
{
    internal static IEnumerable<SignalSource> CreateSignalSources()
    {
        yield return CreateSignalSource(
            id: "1");
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
            sourceId: "1");
    }

    internal static Signal CreateSignal(
        string id,
        string sourceId) => Signal
        .Create(
            SignalId.From(id).ThrowIfFailure().ValueOrDefault,
            SignalSourceId.From(sourceId).ThrowIfFailure().ValueOrDefault)
        .ThrowIfError()
        .ValueOrDefault;

    internal static IEnumerable<SignalSourceSubscription> CreateSignalSourceSubscriptions()
    {
        yield return CreateSignalSourceSubscription(
            id: "1",
            sourceId: "1");
    }

    internal static SignalSourceSubscription CreateSignalSourceSubscription(
        string id,
        string sourceId) =>
        SignalSourceSubscription
            .Create(
                SignalSourceSubscriptionId.From(id).ThrowIfFailure().ValueOrDefault,
                SignalSourceId.From(sourceId).ThrowIfFailure().ValueOrDefault)
            .ThrowIfError()
            .ValueOrDefault;
}