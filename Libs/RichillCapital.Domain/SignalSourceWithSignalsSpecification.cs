using RichillCapital.SharedKernel.Specifications;
using RichillCapital.SharedKernel.Specifications.Builders;

namespace RichillCapital.Domain;

public sealed class SignalSourceWithSignalsSpecification :
    Specification<SignalSource>
{
    public SignalSourceWithSignalsSpecification(
        SignalSourceId sourceId) =>
        Query
            .Where(source => source.Id == sourceId)
            .Include(source => source.Signals);
}