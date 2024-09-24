using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Files;
using RichillCapital.UseCases.Files.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Files;

[ApiVersion(EndpointVersion.V1)]
public sealed class GetFileEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult<FileDto>
{
    [HttpGet(ApiRoutes.Files.Get)]
    [SwaggerOperation(
        Summary = "Get a file",
        Description = "Get a file from the file storage system",
        Tags = [ApiTags.Files])]
    public override async Task<ActionResult<FileDto>> HandleAsync(
        [FromRoute(Name = nameof(fileId))] string fileId,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<string>
            .With(fileId)
            .Then(id => new GetFileQuery
            {
                FileId = id,
            })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Match(HandleFailure, Ok);
}