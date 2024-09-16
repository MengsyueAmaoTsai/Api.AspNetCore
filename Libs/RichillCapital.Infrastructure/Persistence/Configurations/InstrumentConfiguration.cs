using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain;
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

        builder.HasData([
            CreateInstrument("TAIFEX:TXF!1", "Taiwan Futures Exchange Futures", InstrumentType.Equity),
            CreateInstrument("TAIFEX:TEF!1", "Taiwan Electronic Futures", InstrumentType.Future),
            CreateInstrument("TAIFEX:TFF!1", "Taiwan Financial Futures", InstrumentType.Future),
            CreateInstrument("MAX:USDTTWD", "USDT / TWD", InstrumentType.CryptoCurrency),
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