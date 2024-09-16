using Microsoft.AspNetCore.Mvc;

namespace RichillCapital.Contracts.Positions;

public sealed record GetPositionRequest
{
    [FromRoute(Name = "positionId")]
    public required string PositionId { get; init; }
}