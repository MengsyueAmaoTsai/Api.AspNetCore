using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Signals.Queries;

internal sealed class GetSignalQueryHandler(
    IReadOnlyRepository<Signal> _signalRepository) :
    IQueryHandler<GetSignalQuery, ErrorOr<SignalDto>>
{
    public async Task<ErrorOr<SignalDto>> Handle(
        GetSignalQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = SignalId.From(query.SignalId);

        if (validationResult.IsFailure)
        {
            return ErrorOr<SignalDto>.WithError(validationResult.Error);
        }

        var maybeSignal = await _signalRepository.GetByIdAsync(validationResult.Value);

        if (maybeSignal.IsNull)
        {
            var error = Error.NotFound("Signals.NotFound", $"Signal with id {query.SignalId} was not found.");

            return ErrorOr<SignalDto>.WithError(error);
        }

        var signal = maybeSignal.Value;

        return ErrorOr<SignalDto>.With(signal.ToDto());
    }
}
