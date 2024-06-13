using Asp.Versioning;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.SignalSources;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.SignalSources;

[ApiVersion(EndpointVersion.V1)]
public sealed class CreateSignalSourceEndpoint : AsyncEndpoint
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
        CancellationToken cancellationToken = default)
    {
        return Ok(new CreateSignalSourceResponse
        {
            Id = request.Id
        });
    }
}