using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Persistence.Seeds;

internal static partial class Seed
{
    internal static IEnumerable<SignalSource> CreateSignalSources()
    {
        yield return CreateSignalSource("1");
    }

    internal static SignalSource CreateSignalSource(string id) =>
        SignalSource
            .Create(
                SignalSourceId.From(id).ThrowIfFailure().ValueOrDefault)
            .ThrowIfError()
            .ValueOrDefault;
}