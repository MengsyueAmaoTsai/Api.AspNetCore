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
public sealed class CreateUserEndpoint(IMediator _mediator) : AsyncEndpoint
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
    public override async Task<ActionResult<CreateUserResponse>> HandleAsync(
        [FromBody] CreateUserRequest request,
        CancellationToken cancellationToken = default) =>
        await request
            .ToErrorOr()
            .Then(req => req.ToCommand())
            .Then(command => _mediator.Send(command, cancellationToken))
            .Then(id => id.ToResponse())
            .Match(HandleFailure, Ok);
}
