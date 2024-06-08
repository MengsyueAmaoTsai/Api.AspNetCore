using System.Net;
using System.Net.Mime;

using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Files;
using RichillCapital.SharedKernel.Monads;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Files;


[ApiVersion(EndpointVersion.V1)]
public sealed class DownloadFileEndpoint(IMediator _mediator) : AsyncEndpoint
    .WithRequest<DownloadFileRequest>
    .WithActionResult
{
    [HttpGet(ApiRoutes.Files.Download)]
    [MapToApiVersion(EndpointVersion.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Download a file",
        Description = "Download a file by its ID",
        OperationId = "Files.Download",
        Tags = ["Files"])]
    public override async Task<ActionResult> HandleAsync(
        [FromQuery] DownloadFileRequest request,
        CancellationToken cancellationToken = default) =>
        await request
            .ToErrorOr()
            .Then(req => req.ToQuery())
            .Then(query => _mediator.Send(query, cancellationToken))
            .Match(
                HandleFailure,
                dto => File(
                    dto.Content,
                    MediaTypeNames.Application.Octet,
                    WebUtility.HtmlEncode(dto.Name)));
}