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
            .. CreateTaiwanStocks(),
            .. CreateTaiwanFutures(),
            .. CreateUSStocks(),
            .. CreateUSFutures(),
            .. CreateCryptoCurrencies(),
            .. CreateCryptoPerpetuals(),
        ]);
    }

    private static IEnumerable<Instrument> CreateUSStocks()
    {
        yield return CreateInstrument(
            symbol: "NASDAQ:MSFT",
            description: "Microsoft Corporation",
            type: InstrumentType.Equity);

        yield return CreateInstrument(
            symbol: "NASDAQ:AAPL",
            description: "Apple Inc.",
            type: InstrumentType.Equity);

        yield return CreateInstrument(
            symbol: "NASDAQ:AMZN",
            description: "Amazon.com Inc.",
            type: InstrumentType.Equity);

        yield return CreateInstrument(
            symbol: "NASDAQ:FB",
            description: "Meta Platforms Inc.",
            type: InstrumentType.Equity);
    }

    private static IEnumerable<Instrument> CreateUSFutures()
    {
        yield return CreateInstrument(
            symbol: "CME:ES",
            description: "E-mini S&P 500 Futures",
            type: InstrumentType.Future);

        yield return CreateInstrument(
            symbol: "CME:NQ",
            description: "E-mini Nasdaq 100 Futures",
            type: InstrumentType.Future);

        yield return CreateInstrument(
            symbol: "CME:YM",
            description: "E-mini Dow Jones Futures",
            type: InstrumentType.Future);

        yield return CreateInstrument(
            symbol: "CME:RTY",
            description: "E-mini Russell 2000 Futures",
            type: InstrumentType.Future);
    }

    private static IEnumerable<Instrument> CreateTaiwanStocks()
    {
        yield return CreateInstrument(
            symbol: "TWSE:2330",
            description: "Taiwan Semiconductor Manufacturing Company Limited",
            type: InstrumentType.Equity);

        yield return CreateInstrument(
            symbol: "TWSE:2317",
            description: "Hon Hai Precision Industry Co., Ltd.",
            type: InstrumentType.Equity);

        yield return CreateInstrument(
            symbol: "TWSE:2454",
            description: "Mediatek Inc.",
            type: InstrumentType.Equity);

        yield return CreateInstrument(
            symbol: "TWSE:2303",
            description: "United Microelectronics Corporation",
            type: InstrumentType.Equity);
    }

    private static IEnumerable<Instrument> CreateTaiwanFutures()
    {
        yield return CreateInstrument(
            symbol: "TAIFEX:TXF",
            description: "TSE Taiwan 50 Index Futures",
            type: InstrumentType.Future);

        yield return CreateInstrument(
            symbol: "TAIFEX:MXF",
            description: "Mini-TAIEX Futures",
            type: InstrumentType.Future);

        yield return CreateInstrument(
            symbol: "TAIFEX:TEF",
            description: "TSE Electronic Sector Index Futures",
            type: InstrumentType.Future);

        yield return CreateInstrument(
            symbol: "TAIFEX:TFF",
            description: "TSE Finance Sector Index Futures",
            type: InstrumentType.Future);
    }

    private static IEnumerable<Instrument> CreateCryptoCurrencies()
    {
        yield return CreateInstrument(
            symbol: "BINANCE:BTCUSDT",
            description: "Bitcoin / Tether USD",
            type: InstrumentType.CryptoCurrency);

        yield return CreateInstrument(
            symbol: "BINANCE:ETHUSDT",
            description: "Ethereum / Tether USD",
            type: InstrumentType.CryptoCurrency);

        yield return CreateInstrument(
            symbol: "BINANCE:BNBUSDT",
            description: "Binance Coin / Tether USD",
            type: InstrumentType.CryptoCurrency);
    }

    private static IEnumerable<Instrument> CreateCryptoPerpetuals()
    {
        yield return CreateInstrument(
            symbol: "BINANCE:BTCUSDT.P",
            description: "Bitcoin / USD Perpetual",
            type: InstrumentType.Swap);

        yield return CreateInstrument(
            symbol: "BINANCE:ETHUSDT.P",
            description: "Ethereum / USD Perpetual",
            type: InstrumentType.Swap);

        yield return CreateInstrument(
            symbol: "BINANCE:BNBUSDT.P",
            description: "Binance Coin / USD Perpetual",
            type: InstrumentType.Swap);
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