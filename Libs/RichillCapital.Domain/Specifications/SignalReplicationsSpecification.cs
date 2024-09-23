using RichillCapital.SharedKernel.Specifications;
using RichillCapital.SharedKernel.Specifications.Builders;

namespace RichillCapital.Domain.Specifications;

public sealed class SignalReplicationsSpecification : Specification<SignalReplicationPolicy>
{
    public SignalReplicationsSpecification()
    {
        Query.Include(p => p.ReplicationMappings)
            .ThenInclude(m => m.DestinationAccount);
    }
}

