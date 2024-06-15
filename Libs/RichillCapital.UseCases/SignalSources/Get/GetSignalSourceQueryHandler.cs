using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.SignalSources.Get;

internal sealed class GetSignalSourceQueryHandler(
    IReadOnlyRepository<SignalSource> _signalSourceRepository) :
    IQueryHandler<GetSignalSourceQuery, ErrorOr<SignalSourceDto>>
{
    public async Task<ErrorOr<SignalSourceDto>> Handle(
        GetSignalSourceQuery query,
        CancellationToken cancellationToken)
    {
        var idResult = SignalSourceId.From(query.SignalSourceId);

        if (idResult.IsFailure)
        {
            return idResult.Error
                .ToErrorOr<SignalSourceDto>();
        }

        var sourceId = idResult.Value;

        var maybeSignalSource = await _signalSourceRepository.FirstOrDefaultAsync(
            new SignalSourceWithSignalsSpecification(sourceId),
            cancellationToken);

        if (maybeSignalSource.IsNull)
        {
            return SignalSourceErrors
                .NotFound(sourceId)
                .ToErrorOr<SignalSourceDto>();
        }

        var signalSource = maybeSignalSource.Value;

        return signalSource
            .ToDto()
            .ToErrorOr();
    }
}
