using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;
using RichillCapital.Persistence;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Persistence.Configurations;

internal sealed class TradeConfiguration : IEntityTypeConfiguration<Trade>
{
    public void Configure(EntityTypeBuilder<Trade> builder)
    {
        builder
            .HasKey(trade => trade.Id);

        builder
            .Property(trade => trade.Id)
            .HasMaxLength(TradeId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => TradeId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(trade => trade.AccountId)
            .HasMaxLength(AccountId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => AccountId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(trade => trade.Symbol)
            .HasMaxLength(Symbol.MaxLength)
            .HasConversion(
                symbol => symbol.Value,
                value => Symbol.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(trade => trade.Side)
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .HasOne<Account>()
            .WithMany(account => account.Trades)
            .HasForeignKey(trade => trade.AccountId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasData(
        [
            .. CreateTrades_KgiFutures(),
        ]);
    }

    private static IEnumerable<Trade> CreateTrades_KgiFutures()
    {
        var accountId = "000-8283782";
        var symbol = "TAIFEX:TMF";

        yield return CreateTrade(
            id: "1",
            accountId: accountId,
            symbol: symbol,
            side: Side.Short,
            quantity: 1,
            entryPrice: 21719,
            entryTimeUtc: new DateTimeOffset(2024, 9, 12, 11, 18, 32, TimeSpan.Zero),
            exitPrice: 21584,
            exitTimeUtc: new DateTimeOffset(2024, 9, 13, 1, 41, 53, TimeSpan.Zero),
            commission: 32,
            tax: 8,
            swap: 0,
            profitLoss: -1350);

        yield return CreateTrade(
            id: "2",
            accountId: accountId,
            symbol: symbol,
            side: Side.Short,
            quantity: 1,
            entryPrice: 21720,
            entryTimeUtc: new DateTimeOffset(2024, 9, 12, 11, 18, 39, TimeSpan.Zero),
            exitPrice: 21584,
            exitTimeUtc: new DateTimeOffset(2024, 9, 13, 1, 41, 55, TimeSpan.Zero),
            commission: 32,
            tax: 8,
            swap: 0,
            profitLoss: -1340);

        yield return CreateTrade(
            id: "3",
            accountId: accountId,
            symbol: symbol,
            side: Side.Long,
            quantity: 1,
            entryPrice: 21878,
            entryTimeUtc: new DateTimeOffset(2024, 9, 13, 1, 56, 55, TimeSpan.Zero),
            exitPrice: 21858,
            exitTimeUtc: new DateTimeOffset(2024, 9, 13, 1, 57, 07, TimeSpan.Zero),
            commission: 32,
            tax: 8,
            swap: 0,
            profitLoss: -200);

        yield return CreateTrade(
            id: "4",
            accountId: accountId,
            symbol: symbol,
            side: Side.Short,
            quantity: 1,
            entryPrice: 21871,
            entryTimeUtc: new DateTimeOffset(2024, 9, 13, 1, 58, 26, TimeSpan.Zero),
            exitPrice: 21869,
            exitTimeUtc: new DateTimeOffset(2024, 9, 13, 1, 59, 23, TimeSpan.Zero),
            commission: 32,
            tax: 8,
            swap: 0,
            profitLoss: 20);
    }

    private static Trade CreateTrade(
        string id,
        string accountId,
        string symbol,
        Side side,
        decimal quantity,
        decimal entryPrice,
        DateTimeOffset entryTimeUtc,
        decimal exitPrice,
        DateTimeOffset exitTimeUtc,
        decimal commission,
        decimal tax,
        decimal swap,
        decimal profitLoss) =>
        Trade
            .Create(
                TradeId.From(id).ThrowIfFailure().Value,
                AccountId.From(accountId).ThrowIfFailure().Value,
                Symbol.From(symbol).ThrowIfFailure().Value,
                side,
                quantity,
                entryPrice,
                entryTimeUtc,
                exitPrice,
                exitTimeUtc,
                commission,
                tax,
                swap,
                profitLoss)
            .ThrowIfError()
            .Value;
}