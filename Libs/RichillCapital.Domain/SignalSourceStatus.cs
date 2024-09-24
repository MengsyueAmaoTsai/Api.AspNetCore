using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class SignalSourceStatus : Enumeration<SignalSourceStatus>
{
    public static readonly SignalSourceStatus Draft = new(nameof(Draft), 0);
    public static readonly SignalSourceStatus Production = new(nameof(Production), 99);
    private SignalSourceStatus(string name, int value)
        : base(name, value)
    {
    }
}