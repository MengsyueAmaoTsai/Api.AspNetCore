using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Users;
using RichillCapital.SharedKernel.Monads;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Users;

[ApiVersion(EndpointVersion.V1)]
public sealed class ListUserEndpoint(IMediator _mediator) : AsyncEndpoint
    .WithRequest<ListUsersRequest>
    .WithActionResult<Paged<UserResponse>>
{
    [HttpGet(ApiRoutes.Users.List)]
    [MapToApiVersion(EndpointVersion.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "List users",
        Description = "List all users",
        OperationId = "Users.List",
        Tags = ["Users"])]
    public override async Task<ActionResult<Paged<UserResponse>>> HandleAsync(
        [FromQuery] ListUsersRequest request,
        CancellationToken cancellationToken = default) =>
        await request
            .ToErrorOr()
            .Then(req => req.ToQuery())
            .Then(query => _mediator.Send(query, cancellationToken))
            .Then(dto => dto.ToPagedResponse())
            .Match(HandleFailure, Ok);
}
