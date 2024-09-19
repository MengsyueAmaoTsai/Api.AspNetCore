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
            CreateTrade(
                id: "1",
                accountId: "SIM2121844M",
                symbol: "NASDAQ:MSFT",
                side: Side.Long,
                quantity: 1,
                entryPrice: 17000,
                entryTimeUtc: DateTimeOffset.UtcNow,
                exitPrice: 17010,
                exitTimeUtc: DateTimeOffset.UtcNow.AddHours(1),
                commission: 3,
                tax: 5,
                swap: 1,
                profitLoss: 100),
        ]);
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