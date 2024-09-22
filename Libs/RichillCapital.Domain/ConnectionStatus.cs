using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public sealed class ConnectionStatus : Enumeration<ConnectionStatus>
{
    public static readonly ConnectionStatus Stopped = new("Stopped", 0);
    public static readonly ConnectionStatus Active = new("Active", 1);

    private ConnectionStatus(string name, int value)
        : base(name, value)
    {
    }
}