using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Errors;

public static class SignalSubscriptionErrors
{
    public static Error NotFound(SignalSubscriptionId id) =>
        Error.NotFound("SignalSubscriptions.NotFound", $"Signal subscription with id {id} not found.");
}