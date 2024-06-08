using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Users;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Users;


[ApiVersion(EndpointVersion.V1)]
public sealed class ListUsersEndpoint(IMediator _mediator) : AsyncEndpoint
    .WithRequest<ListUsersRequest>
    .WithActionResult<Paged<UserResponse>>
{
    [HttpGet(ApiRoutes.Users.List)]
    [MapToApiVersion(EndpointVersion.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "List users",
        Description = "List users",
        OperationId = "Users.List",
        Tags = ["Users"])]
    public override Task<ActionResult<Paged<UserResponse>>> HandleAsync(
        [FromQuery] ListUsersRequest request,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}