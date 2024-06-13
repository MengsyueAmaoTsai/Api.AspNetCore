using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Api.Endpoints;

namespace RichillCapital.Contracts.SignalSources;

[ApiVersion(EndpointVersion.V1)]
public sealed class GetSignalSourceEndpoint : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult<SignalSourceDetailsResponse>
{
    [HttpGet(ApiRoutes.SignalSource.Get)]
    public override async Task<ActionResult<SignalSourceDetailsResponse>> HandleAsync(
        [FromRoute(Name = "signalSourceId")] string request,
        CancellationToken cancellationToken = default)
    {
        return Ok(new SignalSourceDetailsResponse
        {
            Id = request,
            Name = "ETHUSDT Strategy",
            Description = "ETHUSDT Strategy",
        });
    }
}