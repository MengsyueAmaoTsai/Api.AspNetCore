using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Infrastructure.Brokerages.Max;

internal sealed class MaxSymbolMapper
{
    private const string ExchangePart = "MAX";

    internal Symbol FromExternalSymbol(string externalSymbol) =>
        Symbol.From($"{ExchangePart}:{externalSymbol.ToUpperInvariant()}").ThrowIfFailure().Value;

    internal string ToExternalSymbol(Symbol symbol)
    {
        var symbolPart = symbol.Value.Split(':')[1];

        return symbolPart.ToLowerInvariant();
    }
}