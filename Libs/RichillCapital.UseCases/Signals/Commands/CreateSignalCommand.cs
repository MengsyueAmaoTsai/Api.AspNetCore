
using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Signals.Commands;

public sealed record CreateSignalCommand :
    ICommand<ErrorOr<SignalId>>
{
    public required string SignalSourceId { get; init; }
    public required DateTimeOffset Time { get; init; }
}

internal sealed class CreateSignalCommandHandler(
    IRepository<SignalSource> _signalSourceRepository,
    IRepository<Signal> _signalRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<CreateSignalCommand, ErrorOr<SignalId>>
{
    public async Task<ErrorOr<SignalId>> Handle(
        CreateSignalCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = SignalSourceId.From(command.SignalSourceId);

        if (validationResult.IsFailure)
        {
            return ErrorOr<SignalId>.WithError(validationResult.Error);
        }

        var sourceId = validationResult.Value;
        var maybeSignalSource = await _signalSourceRepository.GetByIdAsync(sourceId, cancellationToken);

        if (maybeSignalSource.IsNull)
        {
            var error = Error.NotFound(
                "SignalSources.NotFound",
                $"Signal source with id {sourceId} not found.");

            return ErrorOr<SignalId>.WithError(error);
        }

        var source = maybeSignalSource.Value;

        var errorOrSignal = Signal.Create(
            SignalId.NewSignalId(),
            source.Id,
            command.Time);

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
