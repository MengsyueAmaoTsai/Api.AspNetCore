using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Trades.Queries;

internal sealed class ListTradesQueryHandler(
    IReadOnlyRepository<Trade> _tradeRepository) :
    IQueryHandler<ListTradesQuery, ErrorOr<IEnumerable<TradeDto>>>
{
    public async Task<ErrorOr<IEnumerable<TradeDto>>> Handle(
        ListTradesQuery query,
        CancellationToken cancellationToken)
    {
        var trades = await _tradeRepository.ListAsync(cancellationToken);

        return ErrorOr<IEnumerable<TradeDto>>.With(trades
            .Select(t => t.ToDto())
            .ToList());
    }
}