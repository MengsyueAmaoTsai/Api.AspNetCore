using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;
using RichillCapital.Persistence;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Persistence.Configurations;

internal sealed class InstrumentConfiguration : IEntityTypeConfiguration<Instrument>
{
    public void Configure(EntityTypeBuilder<Instrument> builder)
    {
        builder
            .HasKey(instrument => instrument.Id);

        builder
            .HasIndex(instrument => instrument.Symbol);

        builder
            .Property(instrument => instrument.Id)
            .HasMaxLength(Symbol.MaxLength)
            .HasConversion(
                symbol => symbol.Value,
                value => Symbol.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(instrument => instrument.Symbol)
            .HasMaxLength(Symbol.MaxLength)
            .HasConversion(
                symbol => symbol.Value,
                value => Symbol.From(value).ThrowIfFailure().Value)
            .IsRequired();

        builder
            .Property(instrument => instrument.Type)
            .HasEnumerationValueConversion()
            .IsRequired();

        builder.HasData([
            CreateInstrument(
                symbol: "MSFT",
                description: "Microsoft Corporation",
                type: InstrumentType.Equity),
            CreateInstrument(
                symbol: "AAPL",
                description: "Apple Inc.",
                type: InstrumentType.Equity),
        ]);
    }

    private static Instrument CreateInstrument(
        string symbol,
        string description,
        InstrumentType type) =>
        Instrument
            .Create(
                Symbol.From(symbol).ThrowIfFailure().Value,
                description,
                type)
            .ThrowIfError()
            .Value;

}