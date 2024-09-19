using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Brokerages.Queries;

public sealed record ListBrokeragesQuery :
    IQuery<ErrorOr<IEnumerable<BrokerageDto>>>
{
}
