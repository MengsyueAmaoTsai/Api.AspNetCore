using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Errors;

public static class InstrumentErrors
{
    public static Error NotFound(Symbol symbol) =>
        Error.NotFound($"Instrument with symbol {symbol} was not found.");
}