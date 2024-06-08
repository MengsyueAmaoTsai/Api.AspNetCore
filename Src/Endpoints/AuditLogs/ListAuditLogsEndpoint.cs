using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.AuditLogs;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.AuditLogs;


[ApiVersion(EndpointVersion.V1)]
public sealed class ListAuditLogsEndpoint(IMediator _mediator) : AsyncEndpoint
    .WithRequest<ListAuditLogsRequest>
    .WithActionResult<Paged<AuditLogResponse>>
{
    [HttpGet(ApiRoutes.AuditLogs.List)]
    [MapToApiVersion(EndpointVersion.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "List audit logs",
        Description = "List audit logs",
        OperationId = "AuditLogs.List",
        Tags = ["AuditLogs"])]
    public override Task<ActionResult<Paged<AuditLogResponse>>> HandleAsync(
        [FromQuery] ListAuditLogsRequest request,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}