using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class OrderId : SingleValueObject<string>
{
    internal const int MaxLength = 36;

    private OrderId(string value) : base(value)
    {
    }

    public static Result<OrderId> From(string value) =>
        Result<string>
            .With(value)
            .Ensure(id => !string.IsNullOrEmpty(id), Error.Invalid($"{nameof(OrderId)} cannot be null or empty."))
            .Ensure(id => id.Length <= MaxLength, Error.Invalid($"{nameof(OrderId)} cannot be longer than {MaxLength} characters."))
            .Then(id => new OrderId(id));

    public static OrderId NewOrderId() =>
        From(Guid.NewGuid().ToString()).ThrowIfFailure().Value;
}