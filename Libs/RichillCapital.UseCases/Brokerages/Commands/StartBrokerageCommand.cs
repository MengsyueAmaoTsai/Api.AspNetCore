using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Brokerages.Commands;

public sealed record StartBrokerageCommand : ICommand<ErrorOr<BrokerageDto>>
{
    public required string ConnectionName { get; init; }
}
