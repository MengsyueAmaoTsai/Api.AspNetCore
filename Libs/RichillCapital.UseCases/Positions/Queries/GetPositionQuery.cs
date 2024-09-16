using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Positions.Queries;

public sealed record GetPositionQuery : 
    IQuery<ErrorOr<PositionDto>>
{
    public required string PositionId { get; init; }
}
