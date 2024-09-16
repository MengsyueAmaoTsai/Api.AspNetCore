using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Trades;

public sealed record ListTradesQuery :
    IQuery<ErrorOr<IEnumerable<TradeDto>>>
{
}
