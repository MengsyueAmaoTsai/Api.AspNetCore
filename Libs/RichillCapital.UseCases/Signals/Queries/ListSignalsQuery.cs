using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Signals.Queries;

public sealed record ListSignalsQuery : IQuery<ErrorOr<IEnumerable<SignalDto>>>
{
}
