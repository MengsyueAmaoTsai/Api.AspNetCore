using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

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

        var id = validationResult.Value;

        var maybeSource = await _signalSourceRepository.FirstOrDefaultAsync(
            source => source.Id == id,
            cancellationToken);

        if (maybeSource.IsNull)
        {
            var error = Error.NotFound(
                "SignalSources.NotFound", 
                $"Signal source with id {id} not found.");

            return ErrorOr<SignalSourceDto>.WithError(error);
        }

        var signalSource = maybeSource.Value;

        return ErrorOr<SignalSourceDto>.With(signalSource.ToDto());
    }
}
