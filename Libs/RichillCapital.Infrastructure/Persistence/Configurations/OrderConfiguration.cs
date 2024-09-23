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
            .. CreateOrders_KgiFutures(),
        ]);
    }

    private static IEnumerable<Order> CreateOrders_KgiFutures()
    {
        var accountId = "000-8283782";
        var symbol = "TAIFEX:TMF";

        yield return CreateOrder(
            id: "1",
            accountId: accountId,
            symbol: symbol,
            tradeType: TradeType.Sell,
            orderType: OrderType.Market,
            timeInForce: TimeInForce.ImmediateOrCancel,
            quantity: 1,
            remainingQuantity: 0,
            executedQuantity: 1,
            status: OrderStatus.Executed,
            createdTimeUtc: new DateTimeOffset(2024, 9, 12, 11, 18, 32, TimeSpan.Zero));

        yield return CreateOrder(
            id: "2",
            accountId: accountId,
            symbol: symbol,
            tradeType: TradeType.Sell,
            orderType: OrderType.Market,
            timeInForce: TimeInForce.ImmediateOrCancel,
            quantity: 1,
            remainingQuantity: 0,
            executedQuantity: 1,
            status: OrderStatus.Executed,
            createdTimeUtc: new DateTimeOffset(2024, 9, 12, 11, 18, 39, TimeSpan.Zero));

        yield return CreateOrder(
            id: "3",
            accountId: accountId,
            symbol: symbol,
            tradeType: TradeType.Buy,
            orderType: OrderType.Market,
            timeInForce: TimeInForce.ImmediateOrCancel,
            quantity: 1,
            remainingQuantity: 0,
            executedQuantity: 1,
            status: OrderStatus.Executed,
            createdTimeUtc: new DateTimeOffset(2024, 9, 13, 1, 41, 53, TimeSpan.Zero));

        yield return CreateOrder(
            id: "4",
            accountId: accountId,
            symbol: symbol,
            tradeType: TradeType.Buy,
            orderType: OrderType.Market,
            timeInForce: TimeInForce.ImmediateOrCancel,
            quantity: 1,
            remainingQuantity: 0,
            executedQuantity: 1,
            status: OrderStatus.Executed,
            createdTimeUtc: new DateTimeOffset(2024, 9, 13, 1, 41, 55, TimeSpan.Zero));


        yield return CreateOrder(
            id: "5",
            accountId: accountId,
            symbol: symbol,
            tradeType: TradeType.Buy,
            orderType: OrderType.Market,
            timeInForce: TimeInForce.ImmediateOrCancel,
            quantity: 1,
            remainingQuantity: 0,
            executedQuantity: 1,
            status: OrderStatus.Executed,
            createdTimeUtc: new DateTimeOffset(2024, 9, 13, 1, 56, 55, TimeSpan.Zero));

        yield return CreateOrder(
            id: "6",
            accountId: accountId,
            symbol: symbol,
            tradeType: TradeType.Sell,
            orderType: OrderType.Market,
            timeInForce: TimeInForce.ImmediateOrCancel,
            quantity: 1,
            remainingQuantity: 0,
            executedQuantity: 1,
            status: OrderStatus.Executed,
            createdTimeUtc: new DateTimeOffset(2024, 9, 13, 1, 56, 07, TimeSpan.Zero));

        yield return CreateOrder(
            id: "7",
            accountId: accountId,
            symbol: symbol,
            tradeType: TradeType.Sell,
            orderType: OrderType.Market,
            timeInForce: TimeInForce.ImmediateOrCancel,
            quantity: 1,
            remainingQuantity: 0,
            executedQuantity: 1,
            status: OrderStatus.Executed,
            createdTimeUtc: new DateTimeOffset(2024, 9, 13, 1, 58, 26, TimeSpan.Zero));

        yield return CreateOrder(
            id: "8",
            accountId: accountId,
            symbol: symbol,
            tradeType: TradeType.Buy,
            orderType: OrderType.Market,
            timeInForce: TimeInForce.ImmediateOrCancel,
            quantity: 1,
            remainingQuantity: 0,
            executedQuantity: 1,
            status: OrderStatus.Executed,
            createdTimeUtc: new DateTimeOffset(2024, 9, 13, 1, 59, 23, TimeSpan.Zero));
    }

    private static Order CreateOrder(
        string id,
        string accountId,
        string symbol,
        TradeType tradeType,
        OrderType orderType,
        TimeInForce timeInForce,
        decimal quantity,
        decimal remainingQuantity,
        decimal executedQuantity,
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
                remainingQuantity,
                executedQuantity,
                status,
                clientOrderId: id,
                createdTimeUtc).ThrowIfError().Value;
}