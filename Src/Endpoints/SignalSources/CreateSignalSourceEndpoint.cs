using Asp.Versioning;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Api.Endpoints;
using RichillCapital.Contracts;
using RichillCapital.Contracts.SignalSources;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Src.Endpoints.SignalSources;

[ApiVersion(EndpointVersion.V1)]
public sealed class CreateSignalSourceEndpoint : AsyncEndpoint
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
    public override Task<ActionResult<CreateSignalSourceResponse>> HandleAsync(
        [FromBody] CreateSignalSourceRequest request,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
