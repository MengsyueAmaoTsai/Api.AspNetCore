using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class TradeId : SingleValueObject<string>
{
    internal const int MaxLength = 36;

    private TradeId(string value)
        : base(value)
    {
    }

    public static Result<TradeId> From(string value) =>
        Result<string>
            .With(value)
            .Then(id => new TradeId(id));

    public static TradeId NewTradeId() =>
        From(Guid.NewGuid().ToString()).ThrowIfFailure().Value;
}