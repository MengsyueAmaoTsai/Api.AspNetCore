using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Position : Entity<PositionId>
{
    private Position(
        PositionId id,
        AccountId accountId,
        Symbol symbol,
        Side side,
        decimal quantity,
        decimal averagePrice,
        decimal commission,
        decimal tax,
        decimal swap,
        PositionStatus status,
        DateTimeOffset createdTimeUtc)
        : base(id)
    {
        AccountId = accountId;
        Symbol = symbol;
        Side = side;
        Quantity = quantity;
        AveragePrice = averagePrice;
        Commission = commission;
        Tax = tax;
        Swap = swap;
        Status = status;
        CreatedTimeUtc = createdTimeUtc;
    }

    public AccountId AccountId { get; private set; }
    public Symbol Symbol { get; private set; }
    public Side Side { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal AveragePrice { get; private set; }
    public decimal Commission { get; private set; }
    public decimal Tax { get; private set; }
    public decimal Swap { get; private set; }
    public PositionStatus Status { get; private set; }
    public DateTimeOffset CreatedTimeUtc { get; private set; }

    public static ErrorOr<Position> Create(
        PositionId id,
        AccountId accountId,
        Symbol symbol,
        Side side,
        decimal quantity,
        decimal averagePrice,
        decimal commission,
        decimal tax,
        decimal swap,
        PositionStatus status,
        DateTimeOffset createdTimeUtc)
    {
        var position = new Position(
            id,
            accountId,
            symbol,
            side,
            quantity,
            averagePrice,
            commission,
            tax,
            swap,
            status,
            createdTimeUtc);

        position.RegisterDomainEvent(new PositionCreatedDomainEvent
        {
            PositionId = position.Id,
            Symbol = position.Symbol,
            Side = position.Side,
            Quantity = position.Quantity,
            AveragePrice = position.AveragePrice,
        });

        return ErrorOr<Position>.With(position);
    }

    public Result Update(
        decimal quantity,
        decimal averagePrice,
        decimal commission,
        decimal tax,
        decimal swap)
    {
        Quantity = quantity;
        AveragePrice = averagePrice;
        Commission = commission;
        Tax = tax;
        Swap = swap;

        RegisterDomainEvent(new PositionUpdatedDomainEvent
        {
            PositionId = Id,
            Symbol = Symbol,
            Side = Side,
            Quantity = Quantity,
            AveragePrice = AveragePrice,
        });

        return Result.Success;
    }

    public Result Close()
    {
        Status = PositionStatus.Closed;

        RegisterDomainEvent(new PositionClosedDomainEvent
        {
            PositionId = Id,
            Symbol = Symbol,
            Side = Side,
            Quantity = Quantity,
            AveragePrice = AveragePrice,
        });

        return Result.Success;
    }

    public bool HasSameDirectionAs(TradeType tradeType) => Side == tradeType.ToSide();

    public Result Increase(
        decimal quantity,
        decimal price,
        decimal commission,
        decimal tax)
    {
        var newQuantity = Quantity + quantity;
        var newAveragePrice = (Quantity * AveragePrice + quantity * price) / newQuantity;

        return Update(newQuantity, newAveragePrice, Commission + commission, Tax + tax, Swap);
    }

    public Result Reduce(
        decimal quantity,
        decimal price,
        decimal commission,
        decimal tax)
    {
        if (Quantity < quantity)
        {
            return Result.Failure(Error.Conflict("Cannot reduce quantity more than current quantity"));
        }

        var newQuantity = Quantity - quantity;
        var newAveragePrice = (Quantity * AveragePrice - quantity * price) / newQuantity;

        return Update(newQuantity, newAveragePrice, Commission + commission, Tax + tax, Swap);
    }
}
