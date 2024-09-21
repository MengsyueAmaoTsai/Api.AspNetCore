using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Brokerages.Commands;

public sealed record DeleteBrokerageCommand : ICommand<Result>
{
    public required string ConnectionName { get; init; }
}
