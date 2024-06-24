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
public sealed class CreateSignalSourceEndpoint(IMediator _mediator) : AsyncEndpoint
    .WithRequest<CreateSignalSourceRequest>
    .WithActionResult<CreateSignalSourceResponse>
{
    [HttpPost(ApiRoutes.SignalSources.Create)]
    [MapToApiVersion(EndpointVersion.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Create a new signal source.",
        Description = "Create a new signal source.",
        OperationId = "SignalSources.Create",
        Tags = ["SignalSources"])]
    public override async Task<ActionResult<CreateSignalSourceResponse>> HandleAsync(
        [FromBody] CreateSignalSourceRequest request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<CreateSignalSourceRequest>
            .With(request)
            .Then(req => req.ToCommand())
            .Then(command => _mediator.Send(command, cancellationToken))
            .Then(id => id.ToResponse())
            .Match(HandleFailure, Ok);
}
