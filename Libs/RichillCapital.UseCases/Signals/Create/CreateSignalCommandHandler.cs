using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Signals.Create;

internal sealed class CreateSignalCommandHandler(
    ILogger<CreateSignalCommandHandler> _logger,
    IRepository<Signal> _signalRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<CreateSignalCommand, ErrorOr<SignalId>>
{
    public async Task<ErrorOr<SignalId>> Handle(
        CreateSignalCommand command,
        CancellationToken cancellationToken)
    {
        var latency = DateTimeOffset.UtcNow - command.Time;

        _logger.LogInformation(
            "Creating signal for {SourceId} with latency {Latency} ms",
            command.SourceId,
            (int)latency.TotalMilliseconds);

        var errorOrSignal = Signal.Create(
            SignalId.NewSignalId(),
            command.SourceId,
            command.Time,
            command.Exchange,
            command.Symbol,
            command.Quantity,
            command.Price,
            "localhost",
            (int)latency.TotalMilliseconds);

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
