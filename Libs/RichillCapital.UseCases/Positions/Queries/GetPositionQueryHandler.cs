using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Positions.Queries;

internal sealed class GetPositionQueryHandler(
    IReadOnlyRepository<Position> _positionRepository) : 
    IQueryHandler<GetPositionQuery, ErrorOr<PositionDto>>
{
    public async Task<ErrorOr<PositionDto>> Handle(
        GetPositionQuery query, 
        CancellationToken cancellationToken)
    {
        var validationResult = PositionId.From(query.PositionId);

        var id = validationResult.Value;

        var maybePosition = await _positionRepository.FirstOrDefaultAsync(
            position => position.Id == id, 
            cancellationToken);
        
        if (maybePosition.IsNull)
        {
            return ErrorOr<PositionDto>.WithError(PositionErrors.NotFound(id));
        }

        var position = maybePosition.Value;

        return ErrorOr<PositionDto>.With(position.ToDto());
    }
}