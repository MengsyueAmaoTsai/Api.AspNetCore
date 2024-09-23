using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Errors;

public static class SignalErrors
{
    public static Error InvalidOrigin(string origin) =>
        Error.Invalid("Signals.InvalidOrigin", $"Invalid origin: {origin}");

    public static Error SourceNotFound(SignalSourceId sourceId) =>
        Error.NotFound("Signals.SourceNotFound", $"Signal source with id {sourceId} not found");
}