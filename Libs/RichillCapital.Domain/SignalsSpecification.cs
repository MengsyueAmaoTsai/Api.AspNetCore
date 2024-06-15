using RichillCapital.SharedKernel.Specifications;
using RichillCapital.SharedKernel.Specifications.Builders;

namespace RichillCapital.Domain;

public sealed class SignalsSpecification : Specification<Signal>
{
    public SignalsSpecification() =>
        Query
            .OrderByDescending(signal => signal.Time);
}