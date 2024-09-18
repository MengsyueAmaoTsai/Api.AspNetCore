using System.Text.RegularExpressions;

using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Symbol : SingleValueObject<string>
{
    private const char Separator = ':';
    private static readonly Regex ExchangePattern = new(@"^[A-Z]+$", RegexOptions.Compiled);
    private static readonly Regex TickerPattern = new(@"^[A-Z0-9!]+(?:\.[A-Z]+)?$", RegexOptions.Compiled);

    internal const int MaxLength = 36;

    private Symbol(string value)
        : base(value)
    {
    }

    public static Result<Symbol> From(string value) =>
        Result<string>
            .With(value)
            .Ensure(symbol => !string.IsNullOrEmpty(symbol), Error.Invalid($"{nameof(Symbol)} cannot be empty."))
            .Ensure(symbol => symbol.Length <= MaxLength, Error.Invalid($"{nameof(Symbol)} cannot be longer than {MaxLength} characters."))
            .Ensure(symbol => symbol.Split(Separator).Length == 2, Error.Invalid($"{nameof(Symbol)} must contain exactly one ':' separating the exchange and ticker, but was '{value}'."))
            .Ensure(
                symbol => ExchangePattern.IsMatch(symbol.Split(Separator)[0]),
                Error.Invalid($"{nameof(Symbol)} exchange part must only contain uppercase letters, but was '{value.Split(Separator)[0]}'."))
            .Ensure(
                symbol => TickerPattern.IsMatch(symbol.Split(Separator)[1]),
                Error.Invalid($"{nameof(Symbol)} ticker part must follow the format with uppercase letters, digits, and special characters '!' or '.' if present, but was '{value.Split(Separator)[1]}'."))
            .Then(symbol => new Symbol(symbol));
}