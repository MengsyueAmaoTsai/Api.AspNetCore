using RichillCapital.Domain.Events;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain;

public sealed class SignalSource : Entity<SignalSourceId>
{
    private readonly List<Signal> _signals = [];
    private readonly List<SignalSubscription> _subscriptions = [];
    private readonly List<SignalReplicationPolicy> _replicationPolicies = [];

    private SignalSource(
        SignalSourceId id,
        string name,
        string description,
        SignalSourceStatus status,
        DateTimeOffset createdTimeUtc)
        : base(id)
    {
        Name = name;
        Description = description;
        Status = status;
        CreatedTimeUtc = createdTimeUtc;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public SignalSourceStatus Status { get; private set; }
    public DateTimeOffset CreatedTimeUtc { get; private set; }

    public IReadOnlyCollection<Signal> Signals => _signals;
    public IReadOnlyCollection<SignalSubscription> Subscriptions => _subscriptions;
    public IReadOnlyCollection<SignalReplicationPolicy> ReplicationPolicies => _replicationPolicies;

    public static ErrorOr<SignalSource> Create(
        SignalSourceId id,
        string name,
        string description,
        SignalSourceStatus status,
        DateTimeOffset createdTimeUtc)
    {
        if (string.IsNullOrEmpty(name))
        {
            return ErrorOr<SignalSource>.WithError(Error.Invalid($"'{nameof(name)}' cannot be null or empty."));
        }

        var source = new SignalSource(
            id,
            name,
            description,
            status,
            createdTimeUtc);

        source.RegisterDomainEvent(new SignalSourceCreatedDomainEvent
        {
            SignalSourceId = source.Id,
        });

        return ErrorOr<SignalSource>.With(source);
    }
}
