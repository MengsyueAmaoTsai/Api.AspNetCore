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
public sealed class DeleteFileEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<DeleteFileRequest>
    .WithActionResult
{
    [HttpDelete(ApiRoutes.Files.Delete)]
    [MapToApiVersion(EndpointVersion.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Delete a file.",
        Description = "Delete a file by its unique identifier.",
        OperationId = "Files.Delete",
        Tags = ["Files"])]
    public override async Task<ActionResult> HandleAsync(
        [FromRoute] DeleteFileRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(
            request.ToCommand(),
            cancellationToken);

        return result
            .Match(NoContent, HandleFailure);
    }
}