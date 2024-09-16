using RichillCapital.Domain;
using RichillCapital.Domain.Abstractions;
using RichillCapital.Domain.Errors;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Abstractions;

namespace RichillCapital.UseCases.Positions.Commands;

internal sealed class ClosePositionCommandHandler(
    IRepository<Position> _positionRepository) :
    ICommandHandler<ClosePositionCommand, ErrorOr<PositionDto>>
{
    public async Task<ErrorOr<PositionDto>> Handle(
        ClosePositionCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = PositionId.From(command.PositionId);

        if (validationResult.IsFailure)
        {
            return ErrorOr<PositionDto>.WithError(validationResult.Error);
        }

        var positionId = validationResult.Value;

        var maybePosition = await _positionRepository
            .FirstOrDefaultAsync(p => p.Id == positionId, cancellationToken);

        if (maybePosition.IsNull)
        {
            return ErrorOr<PositionDto>.WithError(PositionErrors.NotFound(positionId));
        }

        var position = maybePosition.Value;

        return ErrorOr<PositionDto>.With(position.ToDto());
    }
}