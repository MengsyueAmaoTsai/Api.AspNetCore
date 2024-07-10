
using RichillCapital.Domain;

namespace RichillCapital.Persistence.Seeds;

internal static partial class Seed
{
    private static IEnumerable<Instrument> CreateInstruments()
    {
        // Cryptocurrency
        yield return CreateInstrument("BINANCE:BTCUSDT", "BTC/USDT", "Binance");
        yield return CreateInstrument("OKX:BTCUSDT", "BTC/USDT", "Okx");
        yield return CreateInstrument("BYBIT:BTCUSDT", "BTC/USDT", "ByBit");
        yield return CreateInstrument("COINBASE:BTCUSDT", "BTC/USDT", "CoinBase");

        // Perpetual swap
        yield return CreateInstrument("BINANCE:BTCUSDT.P", "BTC/BTC Perpetual", "Binance");
        yield return CreateInstrument("OKX:BTCUSDT.P", "BTC/BTC Perpetual", "Okx");
        yield return CreateInstrument("BYBIT:BTCUSDT.P", "BTC/BTC Perpetual", "ByBit");
        yield return CreateInstrument("COINBASE:BTCUSDT.P", "BTC/BTC Perpetual", "CoinBase");

        // Futures
        yield return CreateInstrument("TAIFEX:TX", "Taiwan Stock Exchange Weighted Index", "TAIFEX");
        yield return CreateInstrument("TAIFEX:TF", "Financial Sector Index Futures", "TAIFEX");
        yield return CreateInstrument("TAIFEX:TE", "Electronic Sector Index Futures", "TAIFEX");
        yield return CreateInstrument("CME:ES", "E-mini S&P 500 Index Futures", "CME");
        yield return CreateInstrument("CME:NQ", "E-mini NASDAQ-100 Index Futures", "CME");
        yield return CreateInstrument("CME:RTY", "E-mini Russell 2000 Index Futures", "CME");

        // ForeX Swap
        yield return CreateInstrument("OANDA:EURUSD", "Euro vs US Dollar", "OANDA");
        yield return CreateInstrument("IC:USDJPY", "US Dollar vs Japanese Yen", "IC Markets");

        // Stocks
        yield return CreateInstrument("NYSE:IBM", "International Business Machines Corporation", "NYSE");
        yield return CreateInstrument("TWSE:2330", "Taiwan Semiconductor Manufacturing Company Limited", "TWSE");
        yield return CreateInstrument("TSE:7203", "Toyota Motor Corporation", "TSE");
    }

    private static Instrument CreateInstrument(
        string symbol,
        string description,
        string exchange) =>
        Instrument.Create(
            Symbol.From(symbol).Value,
            description,
            exchange).Value;
}