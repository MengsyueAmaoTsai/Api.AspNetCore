using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Files;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Files.List;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Files;

[ApiVersion(EndpointVersions.V1)]
public sealed class ListFilesEndpoint(IMediator _mediator) : AsyncEndpoint
    .WithoutRequest
    .WithActionResult<IEnumerable<FileEntryResponse>>
{
    [HttpGet(ApiRoutes.Files.List)]
    [MapToApiVersion(EndpointVersions.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "List all files",
        Description = "List all files uploaded to the system.",
        OperationId = "Files.List",
        Tags = ["Files"])]
    public override async Task<ActionResult<IEnumerable<FileEntryResponse>>> HandleAsync(
        CancellationToken cancellationToken = default) =>
        await _mediator.Send(new ListFilesQuery(), cancellationToken)
            .Then(files => files.ToResponse())
            .Match(HandleFailure, Ok);
}
