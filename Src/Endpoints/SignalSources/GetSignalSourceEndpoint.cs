using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Api.Endpoints;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.SignalSources.Get;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Contracts.SignalSources;


[ApiVersion(EndpointVersion.V1)]
public sealed class GetSignalSourceEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult<SignalSourceDetailsResponse>
{
    [HttpGet(ApiRoutes.SignalSource.Get)]
    [MapToApiVersion(EndpointVersion.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Get a signal source by ID",
        Description = "Get a signal source by ID",
        OperationId = "SignalSource.GetById",
        Tags = ["SignalSource"])]
    public override async Task<ActionResult<SignalSourceDetailsResponse>> HandleAsync(
        [FromRoute(Name = "signalSourceId")] string request,
        CancellationToken cancellationToken = default) =>
        await _mediator
            .Send(new GetSignalSourceQuery
            {
                SignalSourceId = request,
            })
            .Then(dto => dto.ToDetailsResponse())
            .Match(HandleFailure, Ok);
}