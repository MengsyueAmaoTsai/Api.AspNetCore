using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Positions.Commands;

public sealed record ClosePositionCommand : ICommand<ErrorOr<PositionDto>>
{
    public required string PositionId { get; init; }
}
