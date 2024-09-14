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

        builder.HasData(
        [
            CreateExecution(
                "1",
                "BINANCE:BTCUSDT.P",
                TradeType.Buy,
                1,
                43006.2m,
                new DateTimeOffset(2024, 2, 2, 10, 30, 0, TimeSpan.Zero)),

            CreateExecution(
                "2",
                "BINANCE:BTCUSDT.P",
                TradeType.Sell,
                1,
                43049.9m,
                new DateTimeOffset(2024, 2, 2, 10, 30, 0, TimeSpan.Zero)),
        ]);
    }

    private static Execution CreateExecution(
        string id,
        string symbol,
        TradeType tradeType,
        decimal quantity,
        decimal price,
        DateTimeOffset createdTimeUtc) =>
        Execution
            .Create(
                ExecutionId.From(id).ThrowIfFailure().Value,
                Symbol.From(symbol).ThrowIfFailure().Value,
                tradeType,
                quantity,
                price,
                createdTimeUtc)
            .ThrowIfError()
            .Value;
}