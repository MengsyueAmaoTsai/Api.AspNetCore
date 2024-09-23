using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class OrderReplicationMappingId : SingleValueObject<string>
{
    internal const int MaxLength = 36;

    private OrderReplicationMappingId(string value)
        : base(value)
    {
    }

    public static Result<OrderReplicationMappingId> From(string value) =>
        Result<string>
            .With(value)
            .Ensure(
                id => !string.IsNullOrEmpty(id),
                Error.Invalid($"{nameof(OrderReplicationMappingId)} cannot be empty."))
            .Ensure(
                id => id.Length <= MaxLength,
                Error.Invalid($"{nameof(OrderReplicationMappingId)} cannot be longer than {MaxLength} characters."))
            .Then(id => new OrderReplicationMappingId(id));
}