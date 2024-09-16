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
                tradeId => tradeId.Value,
                value => TradeId.From(value).ThrowIfFailure().Value)
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

        builder.HasData(
        [
            CreateTrade("1", "BINANCE:BTCUSDT", PositionSide.Long),
        ]);
    }

    private static Trade CreateTrade(string id, string symbol, PositionSide side) =>
        Trade
            .Create(
                TradeId.From(id).ThrowIfFailure().Value,
                Symbol.From(symbol).ThrowIfFailure().Value,
                side)
            .ThrowIfError()
            .Value;
}