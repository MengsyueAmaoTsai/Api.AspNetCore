using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Brokerages.Queries;

public sealed record GetBrokerageQuery :
    IQuery<ErrorOr<BrokerageDto>>
{
    public required string ConnectionName { get; init; }
}
