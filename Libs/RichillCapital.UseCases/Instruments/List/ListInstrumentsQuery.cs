using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Instruments.List;

public sealed record ListInstrumentsQuery :
    IQuery<ErrorOr<PagedDto<InstrumentDto>>>
{
}
