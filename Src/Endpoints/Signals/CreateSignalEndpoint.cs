using Asp.Versioning;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Signals;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Signals;

[ApiVersion(EndpointVersion.V1)]
public sealed class CreateSignalEndpoint() : AsyncEndpoint
    .WithRequest<CreateSignalRequest>
    .WithActionResult<CreateSignalResponse>
{
    [HttpPost(ApiRoutes.Signals.Create)]
    [MapToApiVersion(EndpointVersion.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Create a signal.",
        Description = "Create a signal.",
        OperationId = "Signals.Create",
        Tags = ["Signals"])]
    public override async Task<ActionResult<CreateSignalResponse>> HandleAsync(
        [FromBody] CreateSignalRequest request, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(new CreateSignalResponse
        {
            Id = Guid.NewGuid().ToString(),
        });
    }
}