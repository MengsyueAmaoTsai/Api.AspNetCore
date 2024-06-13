using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.SignalSources;
using RichillCapital.SharedKernel.Monads;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.SignalSources;


[ApiVersion(EndpointVersion.V1)]
public sealed class CreateSignalSourceEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<CreateSignalSourceRequest>
    .WithActionResult<CreateSignalSourceResponse>
{
    [HttpPost(ApiRoutes.SignalSource.Create)]
    [MapToApiVersion(EndpointVersion.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Creates a new signal source.",
        Description = "Creates a new signal source.",
        OperationId = "SignalSource.Create",
        Tags = ["SignalSource"])]
    public override async Task<ActionResult<CreateSignalSourceResponse>> HandleAsync(
        [FromBody] CreateSignalSourceRequest request,
        CancellationToken cancellationToken = default) =>
        await request
            .ToErrorOr()
            .Then(req => req.ToCommand())
            .Then(command => _mediator.Send(command, cancellationToken))
            .Then(id => id.ToResponse())
            .Match(HandleFailure, Ok);
}