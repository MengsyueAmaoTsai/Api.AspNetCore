using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Signals.Commands;

internal sealed class CreateSignalCommandHandler(
    IRepository<Signal> _signalRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<CreateSignalCommand, ErrorOr<SignalId>>
{
    public async Task<ErrorOr<SignalId>> Handle(
        CreateSignalCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = Result<(SignalSourceId, SignalOrigin)>.Combine(
            SignalSourceId.From(command.SourceId),
            SignalOrigin.FromName(command.Origin)
                .ToResult(Error.Invalid($"Invalid signal origin: {command.Origin}")));

        if (validationResult.IsFailure)
        {
            return ErrorOr<SignalId>.WithError(validationResult.Error);
        }

        var (sourceId, origin) = validationResult.Value;
        var now = DateTimeOffset.UtcNow;

        var errorOrSignal = Signal.Create(
            SignalId.NewSignalId(),
            sourceId,
            command.Time,
            origin,
            now);

        if (errorOrSignal.HasError)
        {
            return ErrorOr<SignalId>.WithError(errorOrSignal.Errors);
        }

        var signal = errorOrSignal.Value;

        _signalRepository.Add(signal);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ErrorOr<SignalId>.With(signal.Id);
    }
}