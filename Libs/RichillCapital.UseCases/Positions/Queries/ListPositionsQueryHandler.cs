using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Positions.Queries;

internal sealed class ListPositionsQueryHandler(
    IReadOnlyRepository<Position> _positionRepository) :
    IQueryHandler<ListPositionsQuery, ErrorOr<IEnumerable<PositionDto>>>
{
    public async Task<ErrorOr<IEnumerable<PositionDto>>> Handle(
        ListPositionsQuery query,
        CancellationToken cancellationToken)
    {
        var positions = await _positionRepository.ListAsync(cancellationToken);

        var result = positions
            .Select(p => p.ToDto())
            .ToList();

        return ErrorOr<IEnumerable<PositionDto>>.With(result);
    }
}