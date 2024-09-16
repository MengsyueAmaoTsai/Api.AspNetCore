using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Errors;

public static class PositionErrors
{
    public static Error NotFound(PositionId id) =>
        Error.NotFound("Positions.NotFound", $"Position with id {id} was not found.");
}
