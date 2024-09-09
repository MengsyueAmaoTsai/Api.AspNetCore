
using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.SignalSources.Queries;

internal sealed class GetSignalSourceQueryHandler(
    IReadOnlyRepository<SignalSource> _signalSourceRepository) :
    IQueryHandler<GetSignalSourceQuery, ErrorOr<SignalSourceDto>>
{
    public async Task<ErrorOr<SignalSourceDto>> Handle(
        GetSignalSourceQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = SignalSourceId.From(query.SignalSourceId);

        if (validationResult.IsFailure)
        {
            return ErrorOr<SignalSourceDto>.WithError(validationResult.Error);
        }

        var sourceId = validationResult.Value;

        var maybeSignalSource = await _signalSourceRepository.GetByIdAsync(
            sourceId,
            cancellationToken);

        if (maybeSignalSource.IsNull)
        {
            return ErrorOr<SignalSourceDto>.WithError(SignalSourceErrors.NotFound(sourceId));
        }
        var dto = maybeSignalSource.Value.ToDto();

        return ErrorOr<SignalSourceDto>.With(dto);
    }
}