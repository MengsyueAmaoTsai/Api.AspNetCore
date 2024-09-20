using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Brokerages;

public static class BrokerageErrors
{
    public static Error NotSupported(string provider) =>
        Error.Invalid("Brokerages.NotSupported", $"Brokerage provider '{provider}' is not supported.");

    public static Error NotFound(string connectionName) =>
        Error.NotFound("Brokerages.NotFound", $"Brokerage '{connectionName}' not found.");

    public static Error AlreadyStarted(string connectionName) =>
        Error.Conflict("Brokerages.AlreadyStarted", $"Brokerage '{connectionName}' is already connected.");

    public static Error AlreadyStopped(string connectionName) =>
        Error.Conflict("Brokerages.AlreadyStopped", $"Brokerage '{connectionName}' is not connected.");
}