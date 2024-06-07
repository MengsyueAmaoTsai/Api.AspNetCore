using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Api.Endpoints;
using RichillCapital.Contracts;
using RichillCapital.Contracts.Files;
using RichillCapital.SharedKernel.Monads;

using Swashbuckle.AspNetCore.Annotations;


[ApiVersion(EndpointVersions.V1)]
public sealed class GetFileEntryEndpoint(IMediator _mediator) : AsyncEndpoint
    .WithRequest<GetFileEntryByIdRequest>
    .WithActionResult<FileEntryDetailsResponse>
{
    [HttpGet(ApiRoutes.Files.Get)]
    [MapToApiVersion(EndpointVersions.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Get file by id",
        Description = "Get file by id.",
        OperationId = "Files.Get",
        Tags = ["Files"])]
    public override async Task<ActionResult<FileEntryDetailsResponse>> HandleAsync(
        [FromRoute] GetFileEntryByIdRequest request,
        CancellationToken cancellationToken = default) =>
        await request
            .ToErrorOr()
            .Then(req => req.ToQuery())
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(dto => dto.ToDetailsResponse())
            .Match(HandleFailure, Ok);
}