using Asp.Versioning;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.SignalSources;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.SignalSources;

[ApiVersion(EndpointVersion.V1)]
public sealed class ListSignalSourcesEndpoint() : AsyncEndpoint
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
        CancellationToken cancellationToken = default)
    {
        return Ok(new Paged<SignalSourceResponse>
        {
            Items =
            [
                new() {
                    Id = "TV-BINANCE:ETHUSDT.P-PL-M15-001",
                    Name = "ETHUSDT Strategy",
                    Description = "ETHUSDT Strategy",
                }
            ]
        });
    }
}