using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Brokerages.Commands;

public sealed record CreateBrokerageCommand :
    ICommand<ErrorOr<BrokerageDto>>
{
    public required string Provider { get; init; }
    public required string Name { get; init; }
    public required IReadOnlyDictionary<string, object> Arguments { get; init; }
}
