using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class SignalReplicationPolicy :
    Entity<SignalReplicationPolicyId>
{
    private readonly List<OrderReplicationMapping> _replicationMapping = [];

    private SignalReplicationPolicy(
        SignalReplicationPolicyId id,
        UserId userId,
        SignalSourceId sourceId,
        decimal multiplier,
        DateTimeOffset createdTimeUtc)
        : base(id)
    {
        UserId = userId;
        SourceId = sourceId;
        Multiplier = multiplier;
        CreatedTimeUtc = createdTimeUtc;
    }

    public UserId UserId { get; private set; }
    public SignalSourceId SourceId { get; private set; }
    public decimal Multiplier { get; private set; }
    public DateTimeOffset CreatedTimeUtc { get; private set; }

    public IReadOnlyList<OrderReplicationMapping> ReplicationMappings => _replicationMapping;

    public static ErrorOr<SignalReplicationPolicy> Create(
        SignalReplicationPolicyId id,
        UserId userId,
        SignalSourceId sourceId,
        decimal multiplier,
        DateTimeOffset createdTimeUtc)
    {
        var policy = new SignalReplicationPolicy(
            id,
            userId,
            sourceId,
            multiplier,
            createdTimeUtc);

        return ErrorOr<SignalReplicationPolicy>.With(policy);
    }
}
