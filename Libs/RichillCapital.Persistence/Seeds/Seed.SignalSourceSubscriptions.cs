using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Persistence.Seeds;

internal static partial class Seed
{
    internal static IEnumerable<SignalSourceSubscription> CreateSignalSourceSubscriptions()
    {
        yield return CreateSignalSourceSubscription("1");
    }

    internal static SignalSourceSubscription CreateSignalSourceSubscription(
        string id) =>
        SignalSourceSubscription
            .Create(
                SignalSourceSubscriptionId.From(id).ThrowIfFailure().ValueOrDefault)
            .ThrowIfError()
            .ValueOrDefault;
}