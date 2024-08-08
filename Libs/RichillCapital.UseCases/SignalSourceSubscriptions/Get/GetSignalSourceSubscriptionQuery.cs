using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.SignalSourceSubscriptions;

public sealed class GetSignalSourceSubscriptionQuery :
    IQuery<ErrorOr<SignalSourceSubscriptionDto>>
{
    public required string SignalSourceSubscriptionId { get; init; }
}
