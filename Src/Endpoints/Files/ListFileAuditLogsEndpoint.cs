using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Api.Endpoints;
using RichillCapital.Contracts;
using RichillCapital.Contracts.AuditLogs;
using RichillCapital.Contracts.Files;
using RichillCapital.SharedKernel.Monads;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Endpoints.Files;


[ApiVersion(EndpointVersion.V1)]
public sealed class ListFileAuditLogsEndpoint(IMediator _mediator) : AsyncEndpoint
    .WithRequest<ListFileAuditLogsRequest>
    .WithActionResult<IEnumerable<AuditLogResponse>>
{
    [HttpGet(ApiRoutes.Files.AuditLogs.List)]
    [MapToApiVersion(EndpointVersion.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "List audit logs for a file.",
        Description = "List audit logs for a file by its unique identifier.",
        OperationId = "Files.AuditLogs.List",
        Tags = ["Files"])]
    public override async Task<ActionResult<IEnumerable<AuditLogResponse>>> HandleAsync(
        [FromRoute] ListFileAuditLogsRequest request,
        CancellationToken cancellationToken = default) =>
        await request
            .ToErrorOr()
            .Then(req => req.ToQuery())
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(logs => logs.Select(log => log.ToResponse()))
            .Match(HandleFailure, Ok);
}