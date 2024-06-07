using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Files;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Files;


[ApiVersion(EndpointVersions.V1)]
public sealed class UploadFileEndpoint(IMediator _mediator) : AsyncEndpoint
    .WithRequest<UploadFileRequest>
    .WithActionResult<UploadFileResponse>
{
    [HttpPost(ApiRoutes.Files.Upload)]
    [MapToApiVersion(EndpointVersions.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Upload a file",
        Description = "Upload a file to the server",
        OperationId = "Files.Upload",
        Tags = ["Files"])]
    public override async Task<ActionResult<UploadFileResponse>> HandleAsync(
        [FromForm] UploadFileRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand();

        var errorOrFileId = await _mediator.Send(command, cancellationToken);

        if (errorOrFileId.HasError)
        {
            return HandleFailure(errorOrFileId.Errors);
        }

        var id = errorOrFileId.Value;

        return Ok(id.ToResponse());
    }
}