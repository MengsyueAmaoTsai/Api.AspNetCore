using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;

namespace RichillCapital.Persistence.Configurations;

internal sealed class InstrumentConfiguration : IEntityTypeConfiguration<Instrument>
{
    public void Configure(EntityTypeBuilder<Instrument> builder)
    {
        builder
            .ToTable("instruments")
            .HasKey(instrument => instrument.Id);

        builder
            .Property(instrument => instrument.Id)
            .HasColumnName("id")
            .HasMaxLength(Symbol.MaxLength)
            .HasConversion(
                id => id.Value,
                value => Symbol.From(value).Value)
            .IsRequired();

        builder
            .Property(instrument => instrument.Symbol)
            .HasColumnName("symbol")
            .HasMaxLength(Symbol.MaxLength)
            .HasConversion(
                symbol => symbol.Value,
                value => Symbol.From(value).Value)
            .IsRequired();

        builder
            .Property(instrument => instrument.Description)
            .HasColumnName("description")
            .IsRequired();

        builder
            .Property(instrument => instrument.Exchange)
            .HasColumnName("exchange")
            .IsRequired();
    }
}