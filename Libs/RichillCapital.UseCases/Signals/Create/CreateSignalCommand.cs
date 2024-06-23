using RichillCapital.Domain.Common.Repositories;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Signals.Create;

public sealed record CreateSignalCommand :
    ICommand<ErrorOr<SignalId>>
{
    public required string SourceId { get; init; }

    public required DateTimeOffset Time { get; init; }

    public required string Exchange { get; init; }

    public required string Symbol { get; init; }

    public required decimal Quantity { get; init; }

    public required decimal Price { get; init; }
}

internal sealed class CreateSignalCommandHandler(
    IRepository<Signal> _signalRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<CreateSignalCommand, ErrorOr<SignalId>>
{
    public async Task<ErrorOr<SignalId>> Handle(
        CreateSignalCommand command,
        CancellationToken cancellationToken)
    {
        var errorOrSignal = Signal.Create(
            SignalId.NewSignalId(),
            command.SourceId,
            command.Time,
            command.Exchange,
            command.Symbol,
            command.Quantity,
            command.Price);

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

public sealed class SignalId : SingleValueObject<Guid>
{
    private SignalId(Guid value)
        : base(value)
    {
    }

    public static Result<SignalId> From(string value) =>
        Result<string>
            .With(value)
            .Then(id => new SignalId(Guid.Parse(id)));

    public static SignalId NewSignalId() => new(Guid.NewGuid());
}

public sealed class Signal : Entity<SignalId>
{
    private Signal(
        SignalId id,
        string sourceId,
        DateTimeOffset time,
        string exchange,
        string symbol,
        decimal quantity,
        decimal price)
        : base(id)
    {
        SourceId = sourceId;
        Time = time;
        Exchange = exchange;
        Symbol = symbol;
        Quantity = quantity;
        Price = price;
    }

    public string SourceId { get; private set; }

    public DateTimeOffset Time { get; private set; }

    public string Exchange { get; private set; }

    public string Symbol { get; private set; }

    public decimal Quantity { get; private set; }

    public decimal Price { get; private set; }

    public static ErrorOr<Signal> Create(
        SignalId id,
        string sourceId,
        DateTimeOffset time,
        string exchange,
        string symbol,
        decimal quantity,
        decimal price)
    {
        var signal = new Signal(
            id,
            sourceId,
            time,
            exchange,
            symbol,
            quantity,
            price);

        return ErrorOr<Signal>.With(signal);
    }
}