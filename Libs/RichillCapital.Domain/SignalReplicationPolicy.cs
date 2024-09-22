using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class SignalReplicationPolicy : Entity<SignalReplicationPolicyId>
{
    private SignalReplicationPolicy(
        SignalReplicationPolicyId id,
        UserId userId,
        SignalSourceId sourceId,
        Symbol tradingSymbol,
        DateTimeOffset createdTimeUtc)
        : base(id)
    {
        UserId = userId;
        TradingSymbol = tradingSymbol;
        SourceId = sourceId;
        CreatedTimeUtc = createdTimeUtc;
    }

    public UserId UserId { get; private set; }
    public SignalSourceId SourceId { get; private set; }
    public Symbol TradingSymbol { get; private set; }
    public DateTimeOffset CreatedTimeUtc { get; private set; }

    public static ErrorOr<SignalReplicationPolicy> Create(
        SignalReplicationPolicyId id,
        UserId userId,
        SignalSourceId sourceId,
        Symbol tradingSymbol,
        DateTimeOffset createdTimeUtc)
    {
        var policy = new SignalReplicationPolicy(
            id,
            userId,
            sourceId,
            tradingSymbol,
            createdTimeUtc);

        return ErrorOr<SignalReplicationPolicy>.With(policy);
    }
}

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
}