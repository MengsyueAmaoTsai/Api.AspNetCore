using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;
using RichillCapital.UseCases.Instruments;

namespace RichillCapital.UseCases.DataFeeds.Queries;

public sealed record ListDataFeedInstrumentsQuery :
    IQuery<ErrorOr<IEnumerable<InstrumentDto>>>
{
    public required string ConnectionName { get; init; }
}
