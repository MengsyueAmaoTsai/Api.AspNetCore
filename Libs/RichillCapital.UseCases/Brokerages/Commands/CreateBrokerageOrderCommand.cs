using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Brokerages.Commands;

public sealed record CreateBrokerageOrderCommand :
    ICommand<ErrorOr<string>>
{
    public required string ConnectionName { get; init; }
}
