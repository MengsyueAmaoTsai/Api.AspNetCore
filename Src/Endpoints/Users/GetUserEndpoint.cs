using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Users;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Users;


[ApiVersion(EndpointVersion.V1)]
public sealed class GetUserEndpoint(IMediator _mediator) : AsyncEndpoint
    .WithRequest<GetUserByIdRequest>
    .WithActionResult<UserDetailsResponse>
{
    [HttpGet(ApiRoutes.Users.Get)]
    [MapToApiVersion(EndpointVersion.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Get a user by ID",
        Description = "Get a user by ID",
        OperationId = "Users.GetById",
        Tags = ["Users"])]
    public override Task<ActionResult<UserDetailsResponse>> HandleAsync(
        [FromRoute] GetUserByIdRequest request,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}