using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;
using RichillCapital.Persistence;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Persistence.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .HasKey(order => order.Id);

        builder
            .Property(order => order.Id)
            .HasMaxLength(OrderId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => OrderId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(order => order.AccountId)
            .HasMaxLength(AccountId.MaxLength)
            .HasConversion(
                accountId => accountId.Value,
                value => AccountId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(order => order.TradeType)
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(order => order.Symbol)
            .HasMaxLength(Symbol.MaxLength)
            .HasConversion(
                symbol => symbol.Value,
                value => Symbol.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(order => order.Type)
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(order => order.TimeInForce)
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(order => order.Status)
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .HasOne(order => order.Account)
            .WithMany(account => account.Orders)
            .HasForeignKey(order => order.AccountId)
            .IsRequired();

        builder.HasData(
        [
            CreateOrder(
                "1",
                accountId: "1",
                TradeType.Buy,
                "BINANCE:BTCUSDT.P",
                OrderType.Market,
                TimeInForce.ImmediateOrCancel,
                1,
                OrderStatus.Executed,
                new DateTimeOffset(2024, 2, 2, 10, 30, 0, TimeSpan.Zero)),

            CreateOrder(
                "2",
                accountId: "1",
                TradeType.Sell,
                "BINANCE:BTCUSDT.P",
                OrderType.Market,
                TimeInForce.ImmediateOrCancel,
                1,
                OrderStatus.Executed,
                new DateTimeOffset(2024, 2, 2, 13, 15, 0, TimeSpan.Zero)),

            CreateOrder(
                "3",
                accountId: "1",
                TradeType.Buy,
                "BINANCE:BTCUSDT.P",
                OrderType.Market,
                TimeInForce.ImmediateOrCancel,
                1,
                OrderStatus.Executed,
                new DateTimeOffset(2024, 2, 2, 20, 0, 0, TimeSpan.Zero)),
        ]);
    }

    private static Order CreateOrder(
        string id,
        string accountId,
        TradeType tradeType,
        string symbol,
        OrderType type,
        TimeInForce timeInForce,
        decimal quantity,
        OrderStatus status,
        DateTimeOffset createdTimeUtc) =>
        Order
            .Create(
                OrderId.From(id).ThrowIfFailure().Value,
                AccountId.From(accountId).ThrowIfFailure().Value,
                Symbol.From(symbol).ThrowIfFailure().Value,
                tradeType,
                type,
                timeInForce,
                quantity,
                status,
                createdTimeUtc)
            .ThrowIfError()
            .Value;
}