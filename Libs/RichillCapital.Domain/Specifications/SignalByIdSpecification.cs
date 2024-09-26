using RichillCapital.SharedKernel.Specifications;
using RichillCapital.SharedKernel.Specifications.Builders;

namespace RichillCapital.Domain.Specifications;

public sealed class SignalByIdSpecification : Specification<Signal>
{
    public SignalByIdSpecification(SignalId signalId)
    {
        Query.Where(signal => signal.Id == signalId);
        Query.Include(signal => signal.Source);
    }
}