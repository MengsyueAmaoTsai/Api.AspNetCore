using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class OrderId : SingleValueObject<string>
{
    internal const int MaxLength = 36;

    private OrderId(string value)
        : base(value)
    {
    }

    public static Result<OrderId> From(string value) =>
        Result<string>
            .With(value)
            .Then(id => new OrderId(id));

    public static OrderId NewOrderId() =>
        From(Guid.NewGuid().ToString()).ThrowIfFailure().Value;
}