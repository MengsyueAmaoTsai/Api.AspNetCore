using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Errors;

public static class SignalSourceErrors
{
    public static Error NotFound(SignalSourceId id) =>
        Error.NotFound("SignalSources.NotFound", $"Signal source with id {id} was not found.");
}