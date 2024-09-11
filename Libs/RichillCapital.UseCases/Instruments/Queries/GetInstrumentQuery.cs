
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Instruments.Queries;

public sealed record GetInstrumentQuery :
    IQuery<ErrorOr<InstrumentDto>>
{
    public required string Symbol { get; init; }
}
