using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Persistence.Seeds;

internal static partial class Seed
{
    internal static IEnumerable<SignalSource> CreateSignalSources()
    {
        yield return CreateSignalSource(
            id: "TV-Long-Task",
            "Signal Source 1",
            "Signal Source 1 description");

        yield return CreateSignalSource(
            id: "TV-Short-Task",
            "Signal Source 2",
            "Signal Source 2 description");

        yield return CreateSignalSource(
            id: "CT-Long-Task",
            "Signal Source 3",
            "Signal Source 3 description");

        yield return CreateSignalSource(
            id: "CT-Short-Task",
            "Signal Source 4",
            "Signal Source 4 description");

        yield return CreateSignalSource(
            id: "MT-Long-Task",
            "Signal Source 5",
            "Signal Source 5 description");

        yield return CreateSignalSource(
            id: "MT-Short-Task",
            "Signal Source 6",
            "Signal Source 6 description");
    }

    internal static SignalSource CreateSignalSource(
        string id,
        string name,
        string description) =>
        SignalSource.Create(
            SignalSourceId.From(id).ThrowIfFailure().Value,
            name,
            description,
            DateTimeOffset.UtcNow).Value;
}