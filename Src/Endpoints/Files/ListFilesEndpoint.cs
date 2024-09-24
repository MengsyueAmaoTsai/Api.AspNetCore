using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Api.Endpoints;
using RichillCapital.Contracts;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Files;
using RichillCapital.UseCases.Files.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Endpoints.Files;

[ApiVersion(EndpointVersion.V1)]
public sealed class ListFilesEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithoutRequest
    .WithActionResult<IEnumerable<FileDto>>
{
    [HttpGet(ApiRoutes.Files.List)]
    [SwaggerOperation(
        Summary = "List all files",
        Description = "List all files in the file storage system",
        OperationId = "Files.List",
        Tags = [ApiTags.Files])]
    public override async Task<ActionResult<IEnumerable<FileDto>>> HandleAsync(
        CancellationToken cancellationToken = default) =>
        await ErrorOr<ListFilesQuery>
            .With(new ListFilesQuery())
            .Then(query => _mediator.Send(query, cancellationToken))
            .Match(HandleFailure, Ok);
}