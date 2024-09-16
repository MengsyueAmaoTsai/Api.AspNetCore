using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;
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

        builder.HasData(
        [
            CreatePosition(
                "1",
                "BINANCE:BTCUSDT.P"),
        ]);
    }

    private static Position CreatePosition(
        string id,
        string symbol) =>
        Position
            .Create(
                PositionId.From(id).ThrowIfFailure().Value,
                Symbol.From(symbol).ThrowIfFailure().Value)
            .ThrowIfError()
            .Value;
}