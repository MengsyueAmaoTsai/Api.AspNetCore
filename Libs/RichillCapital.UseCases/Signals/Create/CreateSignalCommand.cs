using RichillCapital.Domain;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Signals.Create;

public sealed record CreateSignalCommand :
    ICommand<ErrorOr<SignalId>>
{
    public required string SourceId { get; init; }
    public required DateTimeOffset CurrentTime { get; init; }
    public required string Exchange { get; init; }
    public required string Symbol { get; init; }
}
