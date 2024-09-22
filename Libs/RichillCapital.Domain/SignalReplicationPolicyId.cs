using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class SignalReplicationPolicyId : SingleValueObject<string>
{
    internal const int MaxLength = 36;

    private SignalReplicationPolicyId(string value) : base(value)
    {
    }

    public static Result<SignalReplicationPolicyId> From(string value) =>
        Result<string>
            .With(value)
            .Ensure(
                id => !string.IsNullOrWhiteSpace(id),
                Error.Invalid($"'{nameof(value)}' cannot be null or whitespace."))
            .Ensure(
                id => id.Length <= MaxLength,
                Error.Invalid($"'{nameof(value)}' cannot be longer than {MaxLength} characters."))
            .Then(id => new SignalReplicationPolicyId(id));

    public static SignalReplicationPolicyId NewSignalReplicationPolicyId() =>
        From(Guid.NewGuid().ToString()).ThrowIfFailure().Value;
}