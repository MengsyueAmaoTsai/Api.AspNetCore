using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.SignalSources;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.SignalSources;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.SignalSources;


[ApiVersion(EndpointVersion.V1)]
public sealed class ListSignalSourcesEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithoutRequest
    .WithActionResult<Paged<SignalSourceResponse>>
{
    [HttpGet(ApiRoutes.SignalSource.List)]
    [MapToApiVersion(EndpointVersion.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Lists all signal sources.",
        Description = "Lists all signal sources.",
        OperationId = "SignalSource.List",
        Tags = ["SignalSource"])]
    public override async Task<ActionResult<Paged<SignalSourceResponse>>> HandleAsync(
        CancellationToken cancellationToken = default) =>
        await _mediator.Send(new ListSignalSourcesQuery(), cancellationToken)
            .Then(paged => paged.ToPagedResponse())
            .Match(HandleFailure, Ok);
}