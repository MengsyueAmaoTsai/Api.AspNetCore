using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

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

        var id = validationResult.Value;

        var maybeSignal = await _signalRepository.GetByIdAsync(id, cancellationToken);

        if (maybeSignal.IsNull)
        {
            return ErrorOr<SignalDto>.WithError(SignalErrors.NotFound(id));
        }

        return ErrorOr<SignalDto>.With(maybeSignal.Value.ToDto());
    }
}
