
using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.SignalSources.Queries;

internal sealed class ListSignalSourcesQueryHandler(
    IReadOnlyRepository<SignalSource> _signalSourceRepository) :
    IQueryHandler<ListSignalSourcesQuery, ErrorOr<IEnumerable<SignalSourceDto>>>
{
    public async Task<ErrorOr<IEnumerable<SignalSourceDto>>> Handle(
        ListSignalSourcesQuery query,
        CancellationToken cancellationToken)
    {
        var sources = await _signalSourceRepository.ListAsync(cancellationToken);

        var result = sources
            .Select(s => s.ToDto())
            .ToList();

        return ErrorOr<IEnumerable<SignalSourceDto>>.With(result);
    }
}