using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;
using RichillCapital.Persistence;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Persistence.Configurations;

internal sealed class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder
            .HasKey(position => position.Id);

        builder
            .Property(position => position.Id)
            .HasMaxLength(PositionId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => PositionId.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(position => position.Symbol)
            .HasMaxLength(Symbol.MaxLength)
            .HasConversion(
                symbol => symbol.Value,
                value => Symbol.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(position => position.Side)
            .HasEnumerationValueConversion()
            .IsRequired();

        builder.HasData(
        [
            CreatePosition(
                "1",
                "BINANCE:BTCUSDT.P",
                PositionSide.Long,
                1,
                43044.2m),
        ]);
    }

    private static Position CreatePosition(
        string id,
        string symbol,
        PositionSide side,
        decimal quantity,
        decimal averagePrice) =>
        Position
            .Create(
                PositionId.From(id).ThrowIfFailure().Value,
                Symbol.From(symbol).ThrowIfFailure().Value,
                side,
                quantity,
                averagePrice)
            .ThrowIfError()
            .Value;
}