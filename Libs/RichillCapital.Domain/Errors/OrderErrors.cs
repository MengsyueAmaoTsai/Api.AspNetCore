using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Errors;

public static class OrderErrors
{
    public static Error NotFound(OrderId orderId) =>
        Error.NotFound(
            "Orders.NotFound",
            $"Order with id {orderId} was not found.");
}