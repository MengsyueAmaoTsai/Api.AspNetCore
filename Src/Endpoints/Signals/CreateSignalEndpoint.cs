using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Signals;
using RichillCapital.SharedKernel.Monads;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Signals;

[ApiVersion(EndpointVersion.V1)]
public sealed class CreateSignalEndpoint(IMediator _mediator) : AsyncEndpoint
    .WithRequest<CreateSignalRequest>
    .WithActionResult<CreateSignalResponse>
{
    [HttpPost(ApiRoutes.Signals.Create)]
    [MapToApiVersion(EndpointVersion.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Creates a new signal.",
        Description = "Creates a new signal.",
        OperationId = "Signals.Create",
        Tags = ["Signals"])]
    public override async Task<ActionResult<CreateSignalResponse>> HandleAsync(
        [FromBody] CreateSignalRequest request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<CreateSignalRequest>
            .With(request)
            .Then(req => req.ToCommand())
            .Then(command => _mediator.Send(command, cancellationToken))
            .Then(id => id.ToResponse())
            .Match(HandleFailure, Ok);
}