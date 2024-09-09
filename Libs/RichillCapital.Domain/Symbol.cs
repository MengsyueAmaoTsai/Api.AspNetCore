using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class Symbol : SingleValueObject<string>
{
    private Symbol(string value)
        : base(value)
    {
    }
}