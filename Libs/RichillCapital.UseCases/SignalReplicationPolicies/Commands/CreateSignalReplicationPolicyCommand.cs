using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.SignalReplicationPolicies.Commands;

public sealed class CreateSignalReplicationPolicyCommand :
    ICommand<ErrorOr<SignalReplicationPolicyId>>
{
    public required string UserId { get; init; }
    public required string SourceId { get; init; }
    public required decimal Multiplier { get; init; }
}
