
using RichillCapital.Domain;

namespace RichillCapital.Persistence.Seeds;

internal static partial class Seed
{
    private static IEnumerable<Instrument> CreateInstruments()
    {
        // Stock
        yield return CreateInstrument("AAPL", "Apple Inc.", "NASDAQ");

        // Futures
        yield return CreateInstrument("ES", "E-mini S&P 500", "CME");

        // Options
        yield return CreateInstrument("AAPL210917C00130000", "AAPL 2021-09-17 130.00 Call", "NASDAQ");

        // Forex
        yield return CreateInstrument("EURUSD", "Euro / US Dollar", "FOREX");

        // Preptual swap
        yield return CreateInstrument("BTCUSD", "Bitcoin / US Dollar", "CRYPTO");

        // Index
        yield return CreateInstrument("SPX", "S&P 500 Index", "CBOE");

        // ETF
        yield return CreateInstrument("SPY", "SPDR S&P 500 ETF Trust", "NYSE");

        // Bond
        yield return CreateInstrument("US10Y", "10-Year US Treasury Note", "CBOT");

        // Commodity
        yield return CreateInstrument("GC", "Gold", "COMEX");

        // Cryptocurrency
        yield return CreateInstrument("BTC", "Bitcoin", "CRYPTO");

        // Mutual fund
        yield return CreateInstrument("VTSAX", "Vanguard Total Stock Market Index Fund Admiral Shares", "NASDAQ");

        // REIT
        yield return CreateInstrument("VNQ", "Vanguard Real Estate ETF", "NYSE");

        // Forwards
        yield return CreateInstrument("EURUSD.F", "Euro / US Dollar", "FOREX");
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