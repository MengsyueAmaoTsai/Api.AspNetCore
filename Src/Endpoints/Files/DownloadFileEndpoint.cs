using System.Net;
using System.Net.Mime;

using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Files.Queries;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Files;

[ApiVersion(EndpointVersion.V1)]
public sealed class DownloadFileEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult
{
    [HttpGet(ApiRoutes.Files.Download)]
    [SwaggerOperation(
        Summary = "Download a file",
        Description = "Download a file from the server",
        Tags = [ApiTags.Files])]
    public override async Task<ActionResult> HandleAsync(
        [FromRoute(Name = nameof(fileId))] string fileId,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<string>
            .With(fileId)
            .Then(id => new DownloadFileQuery
            {
                FileId = id,
            })
            .Then(query => _mediator.Send(query, cancellationToken))
            .Match(
                HandleFailure,
                file => File(
                    file.Content,
                    MediaTypeNames.Application.Octet,
                    WebUtility.HtmlEncode(file.Name)));
}