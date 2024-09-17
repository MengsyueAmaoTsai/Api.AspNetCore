using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Positions.Queries;

public sealed record ListPositionsQuery : IQuery<ErrorOr<IEnumerable<PositionDto>>>
{
}
