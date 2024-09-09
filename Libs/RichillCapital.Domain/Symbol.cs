using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class Symbol : SingleValueObject<string>
{
    internal const int MaxLength = 36;

    private Symbol(string value)
        : base(value)
    {
    }

    public static Result<Symbol> From(string value) =>
        Result<string>
            .With(value)
            .Then(symbol => new Symbol(symbol));
}