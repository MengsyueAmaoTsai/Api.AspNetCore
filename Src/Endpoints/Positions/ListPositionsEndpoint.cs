using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Positions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Positions.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Positions;

[ApiVersion(EndpointVersion.V1)]
public sealed class ListPositionsEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithoutRequest
    .WithActionResult<IEnumerable<PositionResponse>>
{
    [HttpGet(ApiRoutes.Positions.List)]
    [SwaggerOperation(Tags = [ApiTags.Positions])]
    public override async Task<ActionResult<IEnumerable<PositionResponse>>> HandleAsync(
        CancellationToken cancellationToken = default) =>
        await ErrorOr<ListPositionsQuery>
            .With(new ListPositionsQuery())
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(positions => positions
                .Select(p => p.ToResponse())
                .ToList())
            .Match(HandleFailure, Ok);
}