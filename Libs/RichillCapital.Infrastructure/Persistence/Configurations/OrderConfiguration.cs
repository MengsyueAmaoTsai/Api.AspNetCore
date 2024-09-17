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
                id => id.Value,
                value => AccountId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(order => order.Symbol)
            .HasMaxLength(Symbol.MaxLength)
            .HasConversion(
                symbol => symbol.Value,
                value => Symbol.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(order => order.TradeType)
            .HasEnumerationValueConversion()
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
            .HasOne<Account>()
            .WithMany(account => account.Orders)
            .HasForeignKey(order => order.AccountId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasData(
        [
            .. CreateTradeStationOrders(),
        ]);
    }

    private static IEnumerable<Order> CreateTradeStationOrders()
    {
        var accountId = "SIM2121844M";

        yield return CreateOrder(
            id: "853381720",
            accountId: accountId,
            symbol: "MSFT",
            tradeType: TradeType.Buy,
            orderType: OrderType.Market,
            timeInForce: TimeInForce.ImmediateOrCancel,
            quantity: 5000,
            OrderStatus.Rejected,
            new DateTimeOffset(2024, 9, 17, 17, 53, 19, TimeSpan.Zero));

        yield return CreateOrder(
            id: "853381801",
            accountId: accountId,
            symbol: "MSFT",
            tradeType: TradeType.Buy,
            orderType: OrderType.Market,
            timeInForce: TimeInForce.ImmediateOrCancel,
            quantity: 5000,
            OrderStatus.Executed,
            new DateTimeOffset(2024, 9, 17, 17, 53, 33, TimeSpan.Zero));
    }

    private static Order CreateOrder(
        string id,
        string accountId,
        string symbol,
        TradeType tradeType,
        OrderType orderType,
        TimeInForce timeInForce,
        int quantity,
        OrderStatus status,
        DateTimeOffset createdTimeUtc) =>
        Order.Create(
                OrderId.From(id).ThrowIfFailure().Value,
                AccountId.From(accountId).ThrowIfFailure().Value,
                Symbol.From(symbol).ThrowIfFailure().Value,
                tradeType,
                orderType,
                timeInForce,
                quantity,
                status,
                createdTimeUtc).ThrowIfError().Value;
}