using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Signals.Commands;

internal sealed class CreateSignalCommandHandler(
    IReadOnlyRepository<SignalSource> _signalSourceRepository,
    IRepository<Signal> _signalRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<CreateSignalCommand, ErrorOr<SignalId>>
{
    public async Task<ErrorOr<SignalId>> Handle(
        CreateSignalCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = Result<(SignalSourceId, SignalOrigin, Symbol, TradeType, OrderType)>.Combine(
            SignalSourceId.From(command.SourceId),
            SignalOrigin.FromName(command.Origin)
                .ToResult(Error.Invalid($"Invalid signal origin: {command.Origin}")),
            Symbol.From(command.Symbol),
            TradeType.FromName(command.TradeType)
                .ToResult(Error.Invalid($"Invalid trade type: {command.TradeType}")),
            OrderType.FromName(command.OrderType)
                .ToResult(Error.Invalid($"Invalid order type: {command.OrderType}")));


        if (validationResult.IsFailure)
        {
            return ErrorOr<SignalId>.WithError(validationResult.Error);
        }

        var (sourceId, origin, symbol, tradeType, orderType) = validationResult.Value;

        var maybeSignalSource = await _signalSourceRepository.FirstOrDefaultAsync(
            s => s.Id == sourceId,
            cancellationToken);

        if (maybeSignalSource.IsNull)
        {
            return ErrorOr<SignalId>.WithError(SignalSourceErrors.NotFound(sourceId));
        }

        var now = DateTimeOffset.UtcNow;

        var errorOrSignal = Signal.Create(
            SignalId.NewSignalId(),
            sourceId,
            command.Time,
            origin,
            symbol,
            tradeType,
            orderType,
            command.Quantity,
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