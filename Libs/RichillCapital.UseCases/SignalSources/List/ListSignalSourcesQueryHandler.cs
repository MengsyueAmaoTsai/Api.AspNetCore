
using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.SignalSources;

internal sealed class ListSignalSourcesQueryHandler(
    IReadOnlyRepository<SignalSource> _signalSourceRepository) :
    IQueryHandler<ListSignalSourcesQuery, ErrorOr<PagedDto<SignalSourceDto>>>
{
    public async Task<ErrorOr<PagedDto<SignalSourceDto>>> Handle(
        ListSignalSourcesQuery query,
        CancellationToken cancellationToken)
    {
        var sources = await _signalSourceRepository.ListAsync(cancellationToken);

        return new PagedDto<SignalSourceDto>
        {
            Items = sources.Select(source => source.ToDto()),
        }.ToErrorOr();
    }
}