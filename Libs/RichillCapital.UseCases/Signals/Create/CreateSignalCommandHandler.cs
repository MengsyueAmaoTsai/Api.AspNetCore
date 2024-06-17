using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Signals.Create;

internal sealed class CreateSignalCommandHandler(
    IRepository<SignalSource> _signalSourceRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<CreateSignalCommand, ErrorOr<SignalId>>
{
    public async Task<ErrorOr<SignalId>> Handle(
        CreateSignalCommand command,
        CancellationToken cancellationToken)
    {
        var requestLatency = (int)(DateTimeOffset.UtcNow - command.CurrentTime).TotalMilliseconds;

        var sourceIdResult = SignalSourceId.From(command.SourceId);

        if (sourceIdResult.IsFailure)
        {
            return sourceIdResult.Error
                .ToErrorOr<SignalId>();
        }

        var sourceId = sourceIdResult.Value;

        var errorOrSignal = Signal.Create(
            SignalId.NewSignalId(),
            sourceId,
            command.CurrentTime,
            command.Exchange,
            command.Symbol,
            requestLatency);

        if (errorOrSignal.HasError)
        {
            return errorOrSignal.Errors
                .ToErrorOr<SignalId>();
        }

        var signal = errorOrSignal.Value;

        var maybeSignalSource = await _signalSourceRepository.FirstOrDefaultAsync(
            source => source.Id == sourceId,
            cancellationToken);

        if (maybeSignalSource.IsNull)
        {
            return Error
                .Invalid("Signals.UnknownSource", $"Signal source with id {sourceId} not found.")
                .ToErrorOr<SignalId>();
        }

        var source = maybeSignalSource.Value;

        source.AddSignal(signal);

        // Persist to the database
        _signalSourceRepository.Update(source);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return signal.Id
            .ToErrorOr();
    }
}