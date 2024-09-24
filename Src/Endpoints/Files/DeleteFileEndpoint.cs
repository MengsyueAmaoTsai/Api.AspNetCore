using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Files.Commands;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Files;

[ApiVersion(EndpointVersion.V1)]
public sealed class DeleteFileEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult
{
    [HttpDelete(ApiRoutes.Files.Delete)]
    [SwaggerOperation(
        Summary = "Delete a file",
        Description = "Delete a file from the server",
        Tags = [ApiTags.Files])]
    public override async Task<ActionResult> HandleAsync(
        [FromRoute(Name = nameof(fileId))] string fileId,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<string>
            .With(fileId)
            .Then(id => new DeleteFileCommand
            {
                FileId = id,
            })
            .Then(command => _mediator.Send(command, cancellationToken))
            .Match(HandleFailure, _ => NoContent());
}