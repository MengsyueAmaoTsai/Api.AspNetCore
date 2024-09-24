using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Files;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Files.Commands;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Files;

public sealed class CreateFileEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<CreateFileRequest>
    .WithActionResult<FileCreatedResponse>
{
    [HttpPost(ApiRoutes.Files.Create)]
    [SwaggerOperation(
        Summary = "Creates a new file.",
        Description = "Creates a new file in the file storage system.",
        Tags = [ApiTags.Files])]
    public override async Task<ActionResult<FileCreatedResponse>> HandleAsync(
        [FromForm] CreateFileRequest request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<CreateFileRequest>
            .With(request)
            .Then(req => new CreateFileCommand
            {
                Name = req.Name,
                Description = req.Description,
                Size = req.FromFile.Length,
                FileName = req.FromFile.FileName,
                FileStream = req.FromFile.OpenReadStream(),
                Encrypted = req.Encrypted,
            })
            .Then(command => _mediator.Send(command, cancellationToken))
            .Then(id => new FileCreatedResponse { Id = id.Value })
            .Match(HandleFailure, Ok);
}