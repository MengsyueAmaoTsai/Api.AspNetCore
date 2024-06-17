using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Signals.Create;

internal sealed class CreateSignalCommandHandler(
    IRepository<Signal> _signalRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<CreateSignalCommand, ErrorOr<SignalId>>
{
    public async Task<ErrorOr<SignalId>> Handle(
        CreateSignalCommand command,
        CancellationToken cancellationToken)
    {
        var sourceIdResult = SignalSourceId.From(command.SourceId);

        if (sourceIdResult.IsFailure)
        {
            return sourceIdResult.Error
                .ToErrorOr<SignalId>();
        }

        var latency = (int)(DateTimeOffset.UtcNow - command.CurrentTime).TotalMilliseconds;

        var errorOrSignal = Signal.Create(
            SignalId.NewSignalId(),
            sourceIdResult.Value,
            command.CurrentTime,
            command.Exchange,
            command.Symbol,
            latency);

        if (errorOrSignal.HasError)
        {
            return errorOrSignal.Errors
                .ToErrorOr<SignalId>();
        }

        var signal = errorOrSignal.Value;

        _signalRepository.Add(signal);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return signal.Id
            .ToErrorOr();
    }
}