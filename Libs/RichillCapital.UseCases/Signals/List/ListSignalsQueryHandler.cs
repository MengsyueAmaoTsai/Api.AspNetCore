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
    private const int DefaultPageSize = 50;

    public async Task<ErrorOr<PagedDto<SignalDto>>> Handle(
        ListSignalsQuery query,
        CancellationToken cancellationToken)
    {
        var signals = await _signalRepository.ListAsync(
            new SignalsSpecification(
                query.SearchTerm,
                query.SortBy,
                query.Order),
            cancellationToken);

        var pagedDto = PagedDto<SignalDto>.Create(
            query.Page < 1 ? 1 : query.Page,
            query.PageSize < 1 ? DefaultPageSize : query.PageSize,
            signals.Select(s => s.ToDto()));

        return ErrorOr<PagedDto<SignalDto>>
            .With(pagedDto);
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
        string searchTerm,
        string sortBy,
        string order)
    {
        if (!string.IsNullOrEmpty(searchTerm))
        {
            Query.Where(signal =>
                signal.Symbol.Contains(searchTerm) ||
                signal.Exchange.Contains(searchTerm));
        }

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