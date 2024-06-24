
using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.SharedKernel;
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
        var validationResult = SignalSourceId.From(query.SourceId);

        if (validationResult.IsFailure)
        {
            return ErrorOr<SignalSourceDto>.WithError(validationResult.Error);
        }

        var sourceId = validationResult.Value;

        var maybeSignalSource = await _signalSourceRepository.GetByIdAsync(sourceId, cancellationToken);

        if (maybeSignalSource.IsNull)
        {
            return ErrorOr<SignalSourceDto>.WithError(
                Error.NotFound(
                    "SignalSources.NotFound",
                    $"Signal source with id {sourceId} not found"));
        }

        var signalSource = maybeSignalSource.Value;

        return ErrorOr<SignalSourceDto>.With(signalSource.ToDto());
    }
}