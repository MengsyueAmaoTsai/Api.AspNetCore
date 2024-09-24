using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.SignalSources.Queries;

public sealed record GetSignalSourcePerformanceQuery :
    IQuery<ErrorOr<SignalSourcePerformanceDto>>
{
    public required string SignalSourceId { get; init; }
}
