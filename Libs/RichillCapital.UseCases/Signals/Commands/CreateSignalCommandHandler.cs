using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Signals.Commands;

internal sealed class CreateSignalCommandHandler(
    ILogger<CreateSignalCommandHandler> _logger,
    IReadOnlyRepository<SignalSource> _signalSourceRepository,
    IDateTimeProvider _dateTimeProvider,
    IRepository<Signal> _signalRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<CreateSignalCommand, ErrorOr<SignalId>>
{
    public async Task<ErrorOr<SignalId>> Handle(
        CreateSignalCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = Result<(SignalSourceId, SignalOrigin, Symbol, TradeType, OrderType)>
            .Combine(
                SignalSourceId.From(command.SourceId),
                SignalOrigin.FromName(command.Origin)
                    .ToResult(SignalErrors.IllegalOrigin(command.Origin)),
                Symbol.From(command.Symbol),
                TradeType.FromName(command.TradeType, ignoreCase: true)
                    .ToResult(SignalErrors.InvalidTradeType(command.TradeType)),
                OrderType.FromName(command.OrderType, ignoreCase: true)
                    .ToResult(SignalErrors.InvalidOrderType(command.OrderType)));

        if (validationResult.IsFailure)
        {
            return ErrorOr<SignalId>.WithError(validationResult.Error);
        }

        var (sourceId, origin, symbol, tradeType, orderType) = validationResult.Value;

        if (!await _signalSourceRepository.AnyAsync(s => s.Id == sourceId, cancellationToken))
        {
            return ErrorOr<SignalId>.WithError(SignalErrors.SourceNotFound(sourceId));
        }

        var createdTimeUtc = _dateTimeProvider.UtcNow;
        var latency = (long)(createdTimeUtc - command.Time).TotalMilliseconds;

        _logger.LogInformation("{CreatedTimeUtc} - {Time} = {Latency}", createdTimeUtc, command.Time, latency);

        var errorOrSignal = Signal.Create(
            SignalId.NewSignalId(),
            sourceId,
            origin,
            symbol,
            command.Time,
            tradeType,
            orderType,
            command.Quantity,
            latency,
            createdTimeUtc);

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