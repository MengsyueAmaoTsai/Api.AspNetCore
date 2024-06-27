using System.Linq.Expressions;

using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.SharedKernel.Specifications;
using RichillCapital.SharedKernel.Specifications.Builders;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Signals.List;

internal sealed class ListSignalsQueryHandler(
    IReadOnlyRepository<Signal> _signalRepository) :
    IQueryHandler<ListSignalsQuery, ErrorOr<PagedDto<SignalDto>>>
{
    public async Task<ErrorOr<PagedDto<SignalDto>>> Handle(
        ListSignalsQuery query,
        CancellationToken cancellationToken)
    {
        var signals = await _signalRepository.ListAsync(
            new SignalsSpecification(
                query.SortBy,
                query.Order),
            cancellationToken);

        return ErrorOr<PagedDto<SignalDto>>
            .With(new PagedDto<SignalDto>
            {
                Items = signals
                    .Select(signal => signal.ToDto()),
                TotalCount = signals.Count,
            });
    }
}

/// <summary>
/// Default specification for listing signals.
/// </summary>
internal sealed class SignalsSpecification : Specification<Signal>
{
    private static readonly Expression<Func<Signal, object?>> DefaultKeySelector = signal => signal.Time;
    private const string DefaultOrder = "desc";

    public SignalsSpecification(
        string sortBy,
        string order)
    {
        // Get key selector
        var keySelector = sortBy switch
        {
            "id" => signal => signal.Id,
            "sourceId" => signal => signal.SourceId,
            "time" => signal => signal.Time,
            "symbol" => signal => signal.Symbol,
            "exchange" => signal => signal.Exchange,
            "price" => signal => signal.Price,
            _ => DefaultKeySelector
        };

        // Default order by time descending
        order = string.IsNullOrEmpty(order) ?
            DefaultOrder :
            order.ToLower();

        if (order == "asc")
        {
            Query.OrderBy(keySelector);
        }
        else
        {
            Query.OrderByDescending(keySelector);
        }
    }
}