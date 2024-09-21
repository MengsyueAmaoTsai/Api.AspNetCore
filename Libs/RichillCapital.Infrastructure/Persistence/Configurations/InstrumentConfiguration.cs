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
            .. CreateInstruments_TAIFEX(),
            .. CreateCryptoSwaps(),
        ]);
    }

    private static IEnumerable<Instrument> CreateInstruments_TAIFEX()
    {
        yield return CreateInstrument(
            symbol: "TAIFEX:TXF",
            description: "TAIFEX Futures",
            type: InstrumentType.Future,
            contractUnit: 200);
        yield return CreateInstrument(
            symbol: "TAIFEX:MXF",
            description: "Mini-TAIFEX Futures",
            type: InstrumentType.Future,
            contractUnit: 50);
        yield return CreateInstrument(
            symbol: "TAIFEX:TMF",
            description: "Micro TAIFEX Futures",
            type: InstrumentType.Future,
            contractUnit: 10);

        yield return CreateInstrument(
            symbol: "TAIFEX:EXF",
            description: "TAIFEX Electronic Sector Index Futures",
            type: InstrumentType.Future,
            contractUnit: 4000);
        yield return CreateInstrument(
            symbol: "TAIFEX:ZEF",
            description: "TAIFEX Electronic Sector Index Futures",
            type: InstrumentType.Future,
            contractUnit: 500);

        yield return CreateInstrument(
            symbol: "TAIFEX:FXF",
            description: "TAIFEX Finance Sector Index Futures",
            type: InstrumentType.Future,
            contractUnit: 1000);
        yield return CreateInstrument(
            symbol: "TAIFEX:ZFF",
            description: "Mini TAIFEX Finance Sector Index Futures",
            type: InstrumentType.Future,
            contractUnit: 250);

        yield return CreateInstrument(
            symbol: "TAIFEX:UNF",
            description: "TAIFEX Nasdaq-100 Futures",
            type: InstrumentType.Future,
            contractUnit: 50);
        yield return CreateInstrument(
            symbol: "TAIFEX:UDF",
            description: "TAIFEX Dow Jones Industrial Average Futures",
            type: InstrumentType.Future,
            contractUnit: 20);
        yield return CreateInstrument(
            symbol: "TAIFEX:SPF",
            description: "TAIFEX S&P 500 Futures",
            type: InstrumentType.Future,
            contractUnit: 20);
        yield return CreateInstrument(
            symbol: "TAIFEX:SXF",
            description: "TAIFEX PHLX Semiconductor SectorTM Index",
            type: InstrumentType.Future,
            contractUnit: 80);
    }

    private static IEnumerable<Instrument> CreateCryptoSwaps()
    {
        var instrumentType = InstrumentType.Swap;
        var contractUnit = 1m;

        yield return CreateInstrument(
            symbol: "BINANCE:BTCUSDT.P",
            description: "BTC/USDT Perpetual Swap",
            type: instrumentType,
            contractUnit: contractUnit);
        yield return CreateInstrument(
            symbol: "BINANCE:ETHUSDT.P",
            description: "ETH/USDT Perpetual Swap",
            type: instrumentType,
            contractUnit: contractUnit);
        yield return CreateInstrument(
            symbol: "BINANCE:BNBUSDT.P",
            description: "BNB/USDT Perpetual Swap",
            type: instrumentType,
            contractUnit: contractUnit);
    }

    private static Instrument CreateInstrument(
        string symbol,
        string description,
        InstrumentType type,
        decimal contractUnit) =>
        Instrument
            .Create(
                symbol: Symbol.From(symbol).ThrowIfFailure().Value,
                description: description,
                type: type,
                contractUnit: contractUnit,
                createdTimeUtc: DateTimeOffset.UtcNow)
            .ThrowIfError()
            .Value;
}