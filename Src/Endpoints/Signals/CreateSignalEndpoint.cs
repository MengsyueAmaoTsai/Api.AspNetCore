using Asp.Versioning;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Signals;
using RichillCapital.Notifications;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Signals;

[ApiVersion(EndpointVersion.V1)]
public sealed class CreateSignalEndpoint(ILineNotifyClient _lineNotification) : AsyncEndpoint
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
        await _lineNotification.NotifyAsync($"Signal created. {request.TradeType}");

        return await Task.FromResult(new CreateSignalResponse
        {
            Id = Guid.NewGuid().ToString(),
        });
    }
}