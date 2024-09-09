using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class OrderId : SingleValueObject<string>
{
    private OrderId(string value)
        : base(value)
    {
    }
}