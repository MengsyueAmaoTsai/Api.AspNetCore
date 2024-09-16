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
public sealed class GetPositionEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<GetPositionRequest>
    .WithActionResult<PositionDetailsResponse>
{
    [HttpGet(ApiRoutes.Positions.Get)]
    [SwaggerOperation(Tags = [ApiTags.Positions])]
    public override async Task<ActionResult<PositionDetailsResponse>> HandleAsync(
        GetPositionRequest request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<GetPositionRequest>
            .With(request)
            .Then(req => new GetPositionQuery
            {
                PositionId = req.PositionId,
            })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(position => position.ToDetailsResponse())
            .Match(HandleFailure, Ok);
}