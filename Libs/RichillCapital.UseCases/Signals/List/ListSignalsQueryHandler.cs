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
            new SignalsSpecification(),
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

    public SignalsSpecification()
    {
        // Default order by time descending
        Query
            .OrderByDescending(DefaultKeySelector);
    }
}