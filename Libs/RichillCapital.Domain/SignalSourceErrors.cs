using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public static class SignalSourceErrors
{
    public static Error NotFound(SignalSourceId sourceId) =>
        Error.NotFound("SignalSources.NotFound", $"Signal source with id {sourceId} not found");
}