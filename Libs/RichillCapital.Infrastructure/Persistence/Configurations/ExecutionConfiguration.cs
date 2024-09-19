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
            CreateExecution(
                id: "1",
                accountId: "SIM2121844M",
                orderId: "853434844",
                positionId: "PID1",
                symbol: "NASDAQ:MSFT",
                tradeType: TradeType.Buy,
                orderType: OrderType.Market,
                timeInForce: TimeInForce.ImmediateOrCancel,
                quantity: 500,
                price: 434.88m,
                commission: 5,
                tax: 0,
                createdTimeUtc: new DateTimeOffset(2024, 9, 17, 19, 58, 31, TimeSpan.Zero)),
        ]);
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