using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Api.Endpoints;
using RichillCapital.Contracts;
using RichillCapital.Contracts.Files;
using RichillCapital.SharedKernel.Monads;

using Swashbuckle.AspNetCore.Annotations;

namespace Company.Files.Endpoints.Files;

[ApiVersion(EndpointVersion.V1)]
public sealed class UpdateFileEndpoint(
    IMediator _mediator) : AsyncEndpoint
    .WithRequest<UpdateFileRequest>
    .WithActionResult<FileEntryDetailsResponse>
{
    [HttpPut(ApiRoutes.Files.Update)]
    [MapToApiVersion(EndpointVersion.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Update a file",
        Description = "Update a file by its ID",
        OperationId = "Files.Update",
        Tags = ["Files"])]
    public override async Task<ActionResult<FileEntryDetailsResponse>> HandleAsync(
        [FromRoute] UpdateFileRequest request,
        CancellationToken cancellationToken = default) =>
        await request
            .ToErrorOr()
            .Then(req => req.ToCommand())
            .Then(command => _mediator.Send(command, cancellationToken))
            .Then(dto => dto.ToResponse())
            .Match(HandleFailure, Ok);
}