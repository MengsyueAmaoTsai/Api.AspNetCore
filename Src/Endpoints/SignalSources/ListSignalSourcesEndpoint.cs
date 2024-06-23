using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Api.Endpoints;
using RichillCapital.Contracts;
using RichillCapital.Contracts.SignalSources;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.SignalSources.List;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Src.Endpoints.SignalSources;

[ApiVersion(EndpointVersion.V1)]
public sealed class ListSignalSourcesEndpoint(IMediator _mediator) : AsyncEndpoint
    .WithoutRequest
    .WithActionResult<Paged<SignalSourceResponse>>
{
    [HttpGet(ApiRoutes.SignalSources.List)]
    [MapToApiVersion(EndpointVersion.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "List signal sources.",
        Description = "List signal sources.",
        OperationId = "SignalSources.List",
        Tags = ["SignalSources"])]
    public override async Task<ActionResult<Paged<SignalSourceResponse>>> HandleAsync(
        CancellationToken cancellationToken = default) =>
        await _mediator.Send(new ListSignalSourcesQuery(), cancellationToken)
            .Then(dto => dto.ToPagedResponse())
            .Match(HandleFailure, Ok);
}
