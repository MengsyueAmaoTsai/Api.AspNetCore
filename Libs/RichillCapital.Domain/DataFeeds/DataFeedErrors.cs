using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.DataFeeds;

public static class DataFeedErrors
{
    public static Error AlreadyExists(string connectionName) => Error.Conflict(
        "DataFeeds.AlreadyExists",
        $"Data feed with connection name'{connectionName}' already exists.");

    public static Error NotSupported(string provider) =>
        Error.Invalid("DataFeeds.NotSupported", $"Data feed provider '{provider}' is not supported.");

    public static Error NotFound(string connectionName) =>
        Error.NotFound("DataFeeds.NotFound", $"Data feed '{connectionName}' not found.");

    public static Error AlreadyStarted(string connectionName) =>
        Error.Conflict("DataFeeds.AlreadyStarted", $"Data feed '{connectionName}' is already connected.");

    public static Error AlreadyStopped(string connectionName) =>
        Error.Conflict("DataFeeds.AlreadyStopped", $"Data feed '{connectionName}' is not connected.");
}
