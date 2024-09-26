using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;
using RichillCapital.Persistence;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Persistence.Configurations;

internal sealed class SignalConfiguration : IEntityTypeConfiguration<Signal>
{
    public void Configure(EntityTypeBuilder<Signal> builder)
    {
        builder
            .HasKey(signal => signal.Id);

        builder
            .Property(signal => signal.Id)
            .HasMaxLength(SignalId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => SignalId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(signal => signal.SourceId)
            .HasMaxLength(SignalSourceId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => SignalSourceId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(signal => signal.Origin)
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(signal => signal.Symbol)
            .HasMaxLength(Symbol.MaxLength)
            .HasConversion(
                symbol => symbol.Value,
                value => Symbol.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(signal => signal.TradeType)
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(signal => signal.Status)
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .HasOne(signal => signal.Source)
            .WithMany(source => source.Signals)
            .HasForeignKey(signal => signal.SourceId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasData(
        [
            CreateSignal(
                id: "1",
                sourceId: "TV-Long-Task",
                origin: SignalOrigin.TradingView,
                symbol: "BINANCE:BTCUSDT.P",
                time: DateTimeOffset.Parse("2021-10-01T00:00:00Z"),
                tradeType: TradeType.Buy,
                quantity: 0.1m,
                latency: 1000,
                SignalStatus.Emitted,
                createdTimeUtc: DateTimeOffset.Parse("2021-10-01T00:00:00Z")),
        ]);
    }

    private static Signal CreateSignal(
        string id,
        string sourceId,
        SignalOrigin origin,
        string symbol,
        DateTimeOffset time,
        TradeType tradeType,
        decimal quantity,
        long latency,
        SignalStatus status,
        DateTimeOffset createdTimeUtc) =>
        Signal
            .Create(
                SignalId.From(id).ThrowIfFailure().Value,
                SignalSourceId.From(sourceId).ThrowIfFailure().Value,
                origin,
                Symbol.From(symbol).ThrowIfFailure().Value,
                time,
                tradeType,
                quantity,
                latency,
                status,
                createdTimeUtc)
            .ThrowIfError().Value;
}