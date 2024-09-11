
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Instruments.Queries;

public sealed record ListInstrumentsQuery :
    IQuery<ErrorOr<IEnumerable<InstrumentDto>>>
{
}
