using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.SignalSubscriptions.Queries;

public sealed record GetSignalSubscriptionQuery : IQuery<ErrorOr<SignalSubscriptionDto>>
{
    public required string SignalSubscriptionId { get; init; }
}
