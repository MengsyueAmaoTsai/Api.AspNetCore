using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class SignalReplicationPolicy :
    Entity<SignalReplicationPolicyId>
{
    private SignalReplicationPolicy(
        SignalReplicationPolicyId id,
        UserId userId,
        SignalSourceId sourceId,
        DateTimeOffset createdTimeUtc)
        : base(id)
    {
        UserId = userId;
        SourceId = sourceId;
        CreatedTimeUtc = createdTimeUtc;
    }

    public UserId UserId { get; private set; }
    public SignalSourceId SourceId { get; private set; }
    public DateTimeOffset CreatedTimeUtc { get; private set; }

    public static ErrorOr<SignalReplicationPolicy> Create(
        SignalReplicationPolicyId id,
        UserId userId,
        SignalSourceId sourceId,
        DateTimeOffset createdTimeUtc)
    {
        var policy = new SignalReplicationPolicy(
            id,
            userId,
            sourceId,
            createdTimeUtc);

        return ErrorOr<SignalReplicationPolicy>.With(policy);
    }
}
