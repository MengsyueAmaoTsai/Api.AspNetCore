using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;
using RichillCapital.Persistence;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Persistence.Configurations;

internal sealed class ExecutionConfiguration :
    IEntityTypeConfiguration<Execution>
{
    public void Configure(EntityTypeBuilder<Execution> builder)
    {
        builder
            .HasKey(execution => execution.Id);

        builder
            .Property(execution => execution.Id)
            .HasMaxLength(ExecutionId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => ExecutionId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(execution => execution.AccountId)
            .HasMaxLength(AccountId.MaxLength)
            .HasConversion(
                accountId => accountId.Value,
                value => AccountId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(execution => execution.OrderId)
            .HasMaxLength(OrderId.MaxLength)
            .HasConversion(
                orderId => orderId.Value,
                value => OrderId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(execution => execution.PositionId)
            .HasMaxLength(PositionId.MaxLength)
            .HasConversion(
                positionId => positionId.Value,
                value => PositionId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(execution => execution.Symbol)
            .HasMaxLength(Symbol.MaxLength)
            .HasConversion(
                symbol => symbol.Value,
                value => Symbol.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(execution => execution.TradeType)
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(execution => execution.OrderType)
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(execution => execution.TimeInForce)
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .HasOne<Account>()
            .WithMany(account => account.Executions)
            .HasForeignKey(execution => execution.AccountId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne<Order>()
            .WithMany(order => order.Executions)
            .HasForeignKey(execution => execution.OrderId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder
            .HasOne<Position>()
            .WithMany(position => position.Executions)
            .HasForeignKey(execution => execution.PositionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(
        [
            .. CreateExecutions_KgiFutures(),
        ]);
    }

    private static IEnumerable<Execution> CreateExecutions_KgiFutures()
    {
        var accountId = "000-8283782";
        var symbol = "TAIFEX:TMF";

        yield return CreateExecution(
            id: "00004714",
            accountId: accountId,
            orderId: "1",
            positionId: "PID1",
            symbol: symbol,
            tradeType: TradeType.Sell,
            orderType: OrderType.Market,
            timeInForce: TimeInForce.ImmediateOrCancel,
            quantity: 1,
            price: 21719,
            commission: 16,
            tax: 4,
            createdTimeUtc: new DateTimeOffset(2024, 9, 12, 11, 18, 32, TimeSpan.Zero));

        yield return CreateExecution(
            id: "00004697",
            accountId: accountId,
            orderId: "2",
            positionId: "PID1",
            symbol: symbol,
            tradeType: TradeType.Sell,
            orderType: OrderType.Market,
            timeInForce: TimeInForce.ImmediateOrCancel,
            quantity: 1,
            price: 21720,
            commission: 16,
            tax: 4,
            createdTimeUtc: new DateTimeOffset(2024, 9, 12, 11, 18, 39, TimeSpan.Zero));

        yield return CreateExecution(
            id: "00031776",
            accountId: accountId,
            orderId: "3",
            positionId: "PID1",
            symbol: symbol,
            tradeType: TradeType.Buy,
            orderType: OrderType.Market,
            timeInForce: TimeInForce.ImmediateOrCancel,
            quantity: 1,
            price: 21584,
            commission: 16,
            tax: 4,
            createdTimeUtc: new DateTimeOffset(2024, 9, 13, 1, 41, 53, TimeSpan.Zero));

        yield return CreateExecution(
            id: "00031860",
            accountId: accountId,
            orderId: "4",
            positionId: "PID1",
            symbol: symbol,
            tradeType: TradeType.Buy,
            orderType: OrderType.Market,
            timeInForce: TimeInForce.ImmediateOrCancel,
            quantity: 1,
            price: 21584,
            commission: 16,
            tax: 4,
            createdTimeUtc: new DateTimeOffset(2024, 9, 13, 1, 41, 55, TimeSpan.Zero));

        yield return CreateExecution(
            id: "00032312",
            accountId: accountId,
            orderId: "5",
            positionId: "PID2",
            symbol: symbol,
            tradeType: TradeType.Buy,
            orderType: OrderType.Market,
            timeInForce: TimeInForce.ImmediateOrCancel,
            quantity: 1,
            price: 21878,
            commission: 16,
            tax: 4,
            createdTimeUtc: new DateTimeOffset(2024, 9, 13, 1, 56, 55, TimeSpan.Zero));

        yield return CreateExecution(
            id: "00032434",
            accountId: accountId,
            orderId: "6",
            positionId: "PID2",
            symbol: symbol,
            tradeType: TradeType.Sell,
            orderType: OrderType.Market,
            timeInForce: TimeInForce.ImmediateOrCancel,
            quantity: 1,
            price: 21858,
            commission: 16,
            tax: 4,
            createdTimeUtc: new DateTimeOffset(2024, 9, 13, 1, 57, 07, TimeSpan.Zero));

        yield return CreateExecution(
            id: "00032399",
            accountId: accountId,
            orderId: "7",
            positionId: "PID3",
            symbol: symbol,
            tradeType: TradeType.Sell,
            orderType: OrderType.Market,
            timeInForce: TimeInForce.ImmediateOrCancel,
            quantity: 1,
            price: 21871,
            commission: 16,
            tax: 4,
            createdTimeUtc: new DateTimeOffset(2024, 9, 13, 1, 58, 26, TimeSpan.Zero));

        yield return CreateExecution(
            id: "00032400",
            accountId: accountId,
            orderId: "8",
            positionId: "PID3",
            symbol: symbol,
            tradeType: TradeType.Buy,
            orderType: OrderType.Market,
            timeInForce: TimeInForce.ImmediateOrCancel,
            quantity: 1,
            price: 21869,
            commission: 16,
            tax: 4,
            createdTimeUtc: new DateTimeOffset(2024, 9, 13, 1, 59, 23, TimeSpan.Zero));
    }

    private static Execution CreateExecution(
        string id,
        string accountId,
        string orderId,
        string positionId,
        string symbol,
        TradeType tradeType,
        OrderType orderType,
        TimeInForce timeInForce,
        decimal quantity,
        decimal price,
        decimal commission,
        decimal tax,
        DateTimeOffset createdTimeUtc) =>
        Execution
            .Create(
                ExecutionId.From(id).ThrowIfFailure().Value,
                AccountId.From(accountId).ThrowIfFailure().Value,
                OrderId.From(orderId).ThrowIfFailure().Value,
                PositionId.From(positionId).ThrowIfFailure().Value,
                Symbol.From(symbol).ThrowIfFailure().Value,
                tradeType,
                orderType,
                timeInForce,
                quantity,
                price,
                commission,
                tax,
                createdTimeUtc)
            .ThrowIfError()
            .Value;
}