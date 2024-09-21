using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Errors;

public static class SignalErrors
{
    public static Error InvalidOrigin(string origin) =>
        Error.Invalid("Signals.InvalidOrigin", $"Invalid origin: {origin}");
}