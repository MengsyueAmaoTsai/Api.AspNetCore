using Asp.Versioning;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RichillCapital.Contracts;
using RichillCapital.Contracts.Users;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Api.Endpoints.Users;

[ApiVersion(EndpointVersion.V1)]
public sealed class CreateUserEndpoint : AsyncEndpoint
    .WithRequest<CreateUserRequest>
    .WithActionResult<CreateUserResponse>
{
    [HttpPost(ApiRoutes.Users.Create)]
    [MapToApiVersion(EndpointVersion.V1)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Create user",
        Description = "Create a new user",
        OperationId = "Users.Create",
        Tags = ["Users"])]
    public override Task<ActionResult<CreateUserResponse>> HandleAsync(
        [FromBody] CreateUserRequest request, 
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
