using System.Text.RegularExpressions;

using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Symbol : SingleValueObject<string>
{
    private static readonly char[] AllowedCharactersInSymbolPart = ['!', '.'];
    internal const int MaxLength = 36;

    private Symbol(string value)
        : base(value)
    {
    }

    public static Result<Symbol> From(string value) =>
        Result<string>
            .With(value)
            .Ensure(
                symbol => !string.IsNullOrEmpty(symbol),
                Error.Invalid($"{nameof(Symbol)} cannot be empty."))
            .Ensure(
                symbol => symbol.Length <= MaxLength,
                Error.Invalid($"{nameof(Symbol)} cannot be longer than {MaxLength} characters."))
            .Ensure(
                IsValidSymbolFormat,
                Error.Invalid($"{nameof(Symbol)} must be in the format {{exchange}}:{{ticker}}." +
                    $" Ensure that it contains exactly one colon and both parts are valid."))
            .Then(symbol => new Symbol(symbol));

    private static bool IsValidSymbolFormat(string value)
    {
        var regex = new Regex(@"^(?<exchange>[^:]+):(?<ticker>[a-zA-Z0-9!\.]+)$");
        var match = regex.Match(value);

        return match.Success &&
               !string.IsNullOrEmpty(match.Groups["exchange"].Value) &&
               IsValidTicker(match.Groups["ticker"].Value);
    }

    private static bool IsValidTicker(string value) =>
        new Regex(@"^[a-zA-Z0-9" + Regex.Escape(new string(AllowedCharactersInSymbolPart)) + @"]+$")
            .IsMatch(value);
}