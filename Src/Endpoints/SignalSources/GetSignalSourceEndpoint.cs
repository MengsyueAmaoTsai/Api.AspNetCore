using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.SignalSources;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.SignalSources.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.SignalSources;

[ApiVersion(EndpointVersion.V1)]
public sealed class GetSignalSourceEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<GetSignalSourceRequest>
    .WithActionResult<SignalSourceDetailsResponse>
{
    [HttpGet(ApiRoutes.SignalSources.Get)]
    [SwaggerOperation(Tags = [ApiTags.SignalSources])]
    [Authorize]
    public override async Task<ActionResult<SignalSourceDetailsResponse>> HandleAsync(
        [FromRoute] GetSignalSourceRequest request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<GetSignalSourceRequest>
            .With(request)
            .Then(req => new GetSignalSourceQuery
            {
                SignalSourceId = req.SignalSourceId
            })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(dto => dto.ToDetailsResponse())
            .Match(HandleFailure, Ok);
}